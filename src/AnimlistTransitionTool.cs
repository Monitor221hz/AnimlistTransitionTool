using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Dynamic;

using System.ComponentModel;
namespace Animlist_Transition_Tool;

public partial class AnimlistTransitionTool : Form
{
    //937 > 936 load > draw
    public string filePath = string.Empty;
    public string outputPath = string.Empty;
    public string rootPath;
    public string masterPath;
    public string defmalePath;
    public string deffemalePath;

    BindingSource ToolBindingSource;
    //baseevent = 1217
    //basevar = 230
    public string modName = "OpenSex";
    public string modPrefix = "opnsex";
    public BindingList<AnimDef> AnimDefList;

    public string cropStartVar = "_CropAnimStart";
    public string cropEndVar = "_CropAnimEnd";
    public string startTimeVar = "_AnimStartTime";
    public string playbackSpeedVar = "_AnimationSpeed";
    public string exitEvent = "_ExitAnim";

    public static string transitionFlags = "FLAG_IS_GLOBAL_WILDCARD|FLAG_IS_LOCAL_WILDCARD|FLAG_DISABLE_CONDITION";
    public static string nestedTransitionFlags = "FLAG_IS_GLOBAL_WILDCARD|FLAG_IS_LOCAL_WILDCARD|FLAG_DISABLE_CONDITION";

    //FLAG_TO_NESTED_STATE_ID_IS_VALID
    bool ExportHiddenPatch = true;
    public hkbObject RootState;
    public hkbObject StateMachine;
    
    public AnimlistTransitionTool()
    {
        InitializeComponent();
    }
    public string ReadResource(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        string resourcePath = name;

        resourcePath = assembly.GetManifestResourceNames()
        .Single(str => str.EndsWith(name));
        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        using (StreamReader reader = new StreamReader(stream))
            
        {
            return reader.ReadToEnd();
        }
    }
    public int RandInt()
    {
        Random rnd = new Random();
        string digits = String.Empty;
        for (int i = 0; i < 7; i++)
        {
            digits = digits + rnd.Next(0, 10).ToString();
        }
        return Int32.Parse(digits);
    }
    public void SetOutputFolder()
    {
        using (FolderBrowserDialog FBD = new FolderBrowserDialog())
        {
            FBD.ShowNewFolderButton = true;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                outputPath = FBD.SelectedPath + "\\";
            }
        }
    }
    public void PrepOutput()
    {
        rootPath = outputPath + "\\Nemesis_Engine\\mod\\" + modPrefix;
        if (Directory.Exists(rootPath))
        {
            Directory.Delete(rootPath, true);
        }
        Directory.CreateDirectory(rootPath);
        masterPath = rootPath + "\\0_master";
        deffemalePath = rootPath + "\\defaultfemale";
        defmalePath = rootPath + "\\defaultmale";
        Directory.CreateDirectory(masterPath);
        Directory.CreateDirectory(deffemalePath);
        Directory.CreateDirectory(defmalePath);
    }
    public void OpenTXTFile()
    {
        using (OpenFileDialog OFD = new OpenFileDialog())
        {
            OFD.Title = "Open Animlist File";
            OFD.Filter = "Text files (*.txt)|*.txt";
            //MessageBox.Show("Warning: This version of the tool is designed for porting to the new Ostim framework. Use only on relevant anim packs.");
            if (OFD.ShowDialog() == DialogResult.OK)
            {

                if (OFD.FileName.ToLower().Contains(".txt") && (OFD.FileName.ToLower().Contains("fnis_") || OFD.FileName.ToLower().Contains("att_")))
                {
                    filePath = OFD.FileName;
                    ParseAnimlistData();
                }
                else
                {
                    MessageBox.Show("Warning: Selected file must be a supported FNIS Animlist file.");
                }
            }
        }
        FileViewBox.DataSource = AnimDefList;
        FileViewBox.DisplayMember = "File";
    }
    private void WriteToTXTFile(string path, string text)
    {
        if (!File.Exists(path))
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(text);
                sw.Flush();
                sw.Close();
            }
        }
    }
    private void ExportPatch(hkbObject obj, string path)
    {
        WriteToTXTFile(path + "\\" + obj.GetTag() + ".txt", obj.GetPatch());
    }
    private void ExportPatch(hkbObject obj, string path, string name)
    {
        WriteToTXTFile(path + "\\" + name + ".txt", obj.GetPatch());
    }
    private void ParseAnimlistData(string format = "FNIS")
    {
        AnimDefList = new BindingList<AnimDef>();

        using (StreamReader sr = new StreamReader(filePath))
        {
            string raw = sr.ReadToEnd();
            List<string> Lines = raw.Split('\n').ToList();

            Lines.RemoveAll(x => string.IsNullOrWhiteSpace(x) || x.Contains("\'"));
            foreach (string line in Lines)
            {
                string cleanline = Regex.Replace(line, @"\s{2,}", " ");

                List<string> args = cleanline.Split(" ").ToList();
                string type = args[0];
                List<string> options = args[1].Substring(1).Split(',').ToList();
                string eventname = args[2];
                string file = "Animations\\" + modName + "\\" + args[3].Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\r\n", string.Empty);
                string sex;
                if (options.Contains("f"))
                {
                    sex = "f";
                } else if (options.Contains("m"))
                {
                    sex = "m";
                } else
                {
                    sex = "b";
                }
                
                string mode = "MODE_LOOPING";
                if (options.Contains("a"))
                {
                    mode = "MODE_SINGLE_PLAY";
                } else if (options.Contains("p"))
                {
                    mode = "MODE_PING_PONG";
                }

                List<string> animobj = new List<string>();
                if (args.Count > 4 && options.Contains("o"))
                {
                    List<string> objnames = args.Skip(4).ToList();
                    foreach (string objname in objnames)
                    {
                        animobj.Add(objname.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\r\n", string.Empty));
                    }
                }
                AnimDefList.Add(new AnimDef(type, eventname, file, mode, options, sex, animobj));

            }
        }
    }
    public void ExportAnimlistToPatch()
    {
        
        CultureInfo Defculture = CultureInfo.CurrentCulture;
        CultureInfo.CurrentCulture = new CultureInfo("en-US");

        cropStartVar = modPrefix.ToUpper() + "_CropAnimStart";
        cropEndVar = modPrefix.ToUpper() + "_CropAnimEnd";
        startTimeVar = modPrefix.ToUpper() + "_AnimStartTime";
        playbackSpeedVar = modPrefix.ToUpper() + "_AnimationSpeed";
        exitEvent = "OST_ExitAnim";
        //exitEvent = modPrefix.ToUpper() + "_ExitAnim";
        
        FileProgressBar.Visible = true;
        FileProgressBar.Minimum = 0;
        FileProgressBar.Maximum = AnimDefList.Count * 2+5;
        FileProgressBar.Value = 1;
        FileProgressBar.Step = 1;
        FileProgressBar.PerformStep();
        PrepOutput();
        string s_0106 = ReadResource("#0106.txt");
        string s_0107 = ReadResource("#0107.txt");
        string s_0108 = ReadResource("#0108.txt");
        string s_0340 = ReadResource("#0340.txt");
        string s_f0029 = ReadResource("f#0029.txt");
        string s_m0029 = ReadResource("m#0029.txt");
        string s_2378 = ReadResource("#2378.txt");
        string s_2517 = ReadResource("#2517.txt");
        string s_hkbStateMachineStateInfo = ReadResource("hkbStateMachineStateInfo.txt");
        string s_hkbStateMachine = ReadResource("hkbStateMachine.txt");
        string s_hkbClipGenerator = ReadResource("hkbClipGenerator.txt");
        string s_hkbVariableBindingSet = ReadResource("hkbVariableBindingSet.txt");
        string s_hkTransitionInfo = ReadResource("hkTransitionInfo.txt");
        string s_hkbTransitionInfoArray = ReadResource("hkbTransitionInfoArray.txt");
        string s_hkbBlendingTransitionEffect = ReadResource("hkbBlendingTransitionEffect.txt");
        string s_hkbEvaluateExpressionModifier = ReadResource("hkbEvaluateExpressionModifier.txt");
        string s_hkbExpressionDataArray = ReadResource("hkbExpressionDataArray.txt");


        string s_hkbActiveVariableBindingSet = ReadResource("hkbActiveVariableBindingSet.txt");
        string s_hkbStateMachineEventPropertyArray = ReadResource("hkbStateMachineEventPropertyArray.txt");
        string s_hkEventProperty = ReadResource("hkEventProperty.txt");
        string s_hkbStringEventPayload = ReadResource("hkbStringEventPayload.txt");
        string s_hkbModifierGenerator = ReadResource("hkbModifierGenerator.txt");
        string s_BSIsActiveModifier = ReadResource("BSIsActiveModifier.txt");


        string s_hkcString = "				<hkcstring>{0}</hkcstring>";
        string s_hkcFlags =
@"				<hkobject>
					<hkparam name=""flags"">0</hkparam>
                </hkobject>
";   
        

        RootState = new hkbObject(modPrefix, 0, s_hkbStateMachineStateInfo);
        StateMachine = new hkbObject(modPrefix, 1, s_hkbStateMachine);
        hkbObject DefaultExpressionModifier = new hkbObject(modPrefix, 2, s_hkbEvaluateExpressionModifier);
        hkbObject DefaultExpression = new hkbObject(modPrefix, 3, s_hkbExpressionDataArray);
        DefaultExpression.Params.Tag = DefaultExpression.GetTag();
        DefaultExpression.Params.Expression = playbackSpeedVar;

        DefaultExpressionModifier.Params.Tag = DefaultExpressionModifier.GetTag();
        DefaultExpressionModifier.Params.Name = modName + "_DefaultModifier";
        DefaultExpressionModifier.Params.ExpressionTag = DefaultExpression.GetTag();
        ExportPatch(DefaultExpression, masterPath);
        ExportPatch(DefaultExpressionModifier, masterPath);

        hkbObject _0106 = new hkbObject(modPrefix, s_0106);
        hkbObject _0107 = new hkbObject(modPrefix, s_0107);
        hkbObject _0108 = new hkbObject(modPrefix, s_0108);
        hkbObject _0340 = new hkbObject(modPrefix, s_0340);
        hkbObject _f0029 = new hkbObject(modPrefix, s_f0029);
        hkbObject _m0029 = new hkbObject(modPrefix, s_m0029);
        hkbObject _2378 = new hkbObject(modPrefix, s_2378);
        hkbObject _2517 = new hkbObject(modPrefix, s_2517);
        

        _0340.Params.Prefix = modPrefix;
        _0340.Params.Insert = RootState.GetTag();
        ExportPatch(_0340, masterPath, "#0340");
        

        _2517.Params.Modifier = DefaultExpressionModifier.GetTag();
        ExportPatch(_2517, masterPath, "#2517");

        RootState.Params.Tag = RootState.GetTag();
        RootState.Params.variableBindingSet = null;
        RootState.Params.enterNotifyEvents = null;
        RootState.Params.exitNotifyEvents = null;
        RootState.Params.transitions = null;
        RootState.Params.generator = StateMachine.GetTag();
        RootState.Params.name = modName + "_State";
        RootState.Params.stateID = RandInt();

        List<string> States = new List<string>();
        List<string> EventNames = new List<string>();
        List<string> VarNames = new List<string>();
        List<string> FileNames = new List<string>();
        List<string> Flags = new List<string>();
        List<string> SexExclusions = new List<string>();
        EventNames.Add(String.Format(s_hkcString, exitEvent));
        VarNames.Add(String.Format(s_hkcString, playbackSpeedVar));
        VarNames.Add(String.Format(s_hkcString, cropStartVar));
        VarNames.Add(String.Format(s_hkcString, cropEndVar));
        VarNames.Add(String.Format(s_hkcString, startTimeVar));
        int clipcount = -1;
        
        int id = 4;
        foreach (AnimDef Def in AnimDefList)
        {
            clipcount++;
            Def.BindingSet = new hkbObject(modPrefix, id+3, s_hkbVariableBindingSet);
            Def.State = new hkbObject(modPrefix, id, s_hkbStateMachineStateInfo);
            Def.Clip = new hkbObject(modPrefix, id+2, s_hkbClipGenerator);
            Def.ModifierRoot = new hkbObject(modPrefix, id + 1, s_hkbModifierGenerator);
            Def.Modifier = new hkbObject(modPrefix, id+4, s_BSIsActiveModifier);
            Def.ModifierBindingSet = new hkbObject(modPrefix, id + 5, s_hkbActiveVariableBindingSet);
            
            
            Def.ModifierBindingSet.Params.Prefix = Def.ModifierBindingSet.GetTag();

            Def.Modifier.Params.Prefix = Def.Modifier.GetTag();
            Def.Modifier.Params.BindingSet = Def.ModifierBindingSet.GetTag();
            Def.Modifier.Params.Name = modName + "_BSIsActiveModifier" + Def.Modifier.ID;
            
            
            Def.ModifierRoot.Params.Prefix = Def.ModifierRoot.GetTag();
            Def.ModifierRoot.Params.Name = modName + "_Modifier"+Def.Modifier.ID;
            Def.ModifierRoot.Params.Modifier = Def.Modifier.GetTag();
            Def.ModifierRoot.Params.Generator = Def.Clip.GetTag();

            Def.BindingSet.Params.Tag = Def.BindingSet.GetTag();
            Def.BindingSet.Params.cropStartVar = cropStartVar;
            Def.BindingSet.Params.cropEndVar = cropEndVar;
            Def.BindingSet.Params.startTimeVar = startTimeVar;
            Def.BindingSet.Params.playbackSpeedVar = playbackSpeedVar;

            Def.State.Params.Tag = Def.State.GetTag();
            Def.State.Params.variableBindingSet = null;
            Def.State.Params.enterNotifyEvents = null;
            Def.State.Params.exitNotifyEvents = null;
            Def.State.Params.transitions = null;
            //Def.State.Params.generator = Def.Clip.GetTag();
            Def.State.Params.generator = Def.ModifierRoot.GetTag();
            Def.State.Params.name = modName + "_Anim"+ (clipcount).ToString();
            Def.State.Params.stateID = clipcount;
            hkbObject EventProperty;
            if (Def.AnimObjects.Count > 0)
            {
                List<string> ExitProperties = new List<string>();
                List<string> EnterProperties = new List<string>();
                int p = 0;
                foreach (string obj in Def.AnimObjects)
                {
                    
                    hkbObject Payload = new hkbObject(modPrefix, id + 6, s_hkbStringEventPayload);
                    Payload.Params.Tag = Payload.GetTag();
                    Payload.Params.Name = obj;
                    ExportPatch(Payload, masterPath);

                    EventProperty = new hkbObject(modPrefix, -1, s_hkEventProperty);
                    EventProperty.Params.ID = 937;
                    EventProperty.Params.name = Payload.GetTag();
                    EnterProperties.Add(EventProperty.GetPatch());

                    EventProperty = new hkbObject(modPrefix, -1, s_hkEventProperty);
                    EventProperty.Params.ID = 936;
                    EventProperty.Params.name = Payload.GetTag();
                    EnterProperties.Add(EventProperty.GetPatch());
                    id++;
                }
                EventProperty = new hkbObject(modPrefix, -1, s_hkEventProperty);
                EventProperty.Params.ID = 543;
                EventProperty.Params.name = null;

                ExitProperties.Add(EventProperty.GetPatch());
                hkbObject EnterEventPropertyArray = new hkbObject(modPrefix, id + 6, s_hkbStateMachineEventPropertyArray);
                hkbObject ExitEventPropertyArray = new hkbObject(modPrefix, id + 7, s_hkbStateMachineEventPropertyArray);
                id+=2;
                EnterEventPropertyArray.Params.Tag = EnterEventPropertyArray.GetTag();
                ExitEventPropertyArray.Params.Tag = ExitEventPropertyArray.GetTag();

                EnterEventPropertyArray.Params.numElements = EnterProperties.Count;
                ExitEventPropertyArray.Params.numElements = ExitProperties.Count;

                EnterEventPropertyArray.Params.EventProperties = String.Join("\n", EnterProperties);
                ExitEventPropertyArray.Params.ExitProperties = String.Join("\n", ExitProperties);

                Def.State.Params.exitNotifyEvents = ExitEventPropertyArray.GetTag();
                Def.State.Params.enterNotifyEvents = EnterEventPropertyArray.GetTag();

                ExportPatch(EnterEventPropertyArray, masterPath);
                ExportPatch(ExitEventPropertyArray, masterPath);
            }

            Def.Clip.Params.Tag = Def.Clip.GetTag();
            Def.Clip.Params.variableBindingSet = Def.BindingSet.GetTag();
            Def.Clip.Params.name = modName + "_AnimClip" + (clipcount).ToString();
            Def.Clip.Params.animName = Def.File;
            Def.Clip.Params.triggers = null;
            Def.Clip.Params.cropStartAnimationLocalTime = "0.000000";
            Def.Clip.Params.cropEndAnimationLocalTime = "0.000000";
            Def.Clip.Params.startTime = "0.000000";
            Def.Clip.Params.playbackSpeed = "1.000000";
            Def.Clip.Params.mode = Def.PlayMode;
            Def.Clip.Params.flags = 0;
            id += 6;
            ExportPatch(Def.State, masterPath);
            ExportPatch(Def.ModifierBindingSet, masterPath);
            ExportPatch(Def.ModifierRoot,masterPath);
            ExportPatch(Def.Modifier, masterPath);
            ExportPatch(Def.Clip, masterPath);
            ExportPatch(Def.BindingSet, masterPath);
            States.Add(Def.State.GetTag());
            EventNames.Add(String.Format(s_hkcString, Def.EventName));
            SexExclusions.Add(Def.Sex);
            FileNames.Add(String.Format(s_hkcString, Def.File));
            //Flags.Add
            FileProgressBar.PerformStep();

        }
        FileProgressBar.PerformStep();
        StateMachine.Params.Tag = StateMachine.GetTag();
        StateMachine.Params.name = modName + "_Root";
        StateMachine.Params.numStates = States.Count;
        StateMachine.Params.states = "				" + String.Join(" ", States);
        
        

        List<string> RootTransitionInfos = new List<string>();
        hkbObject RootTransitionInfoArray = new hkbObject(modPrefix, id, s_hkbTransitionInfoArray);
        hkbObject ExitTransitionInfoArray = new hkbObject(modPrefix, id+1, s_hkbTransitionInfoArray);
        id+=2;
        
        hkbObject ExitTransitionInfo = new hkbObject(modPrefix, -1, s_hkTransitionInfo);
        hkbObject ExitTransitionEffect = new hkbObject(modPrefix, id, s_hkbBlendingTransitionEffect);
        ExitTransitionEffect.Params.Tag = ExitTransitionEffect.GetTag();
        ExitTransitionEffect.Params.variableBindingSet = null;
        ExitTransitionEffect.Params.name = "ExitTransition";
        ExitTransitionEffect.Params.duration = "0.200000";

        ExitTransitionInfo.Params.transition = ExitTransitionEffect.GetTag();
        ExitTransitionInfo.Params.eventId = exitEvent;
        ExitTransitionInfo.Params.toStateId = 14;
        ExitTransitionInfo.Params.toNestedStateId = 0;
        ExitTransitionInfo.Params.flags = "FLAG_IS_LOCAL_WILDCARD|FLAG_DISABLE_CONDITION";


        ExitTransitionInfoArray.Params.name = ExitTransitionInfoArray.GetTag();
        ExitTransitionInfoArray.Params.numElements = 1;
        ExitTransitionInfoArray.Params.transitions = ExitTransitionInfo.GetPatch();
        //RootTransitionInfos.Add(ExitTransitionInfo.GetPatch());
        id++;
        ExportPatch(ExitTransitionEffect, masterPath);
        ExportPatch(ExitTransitionInfoArray, masterPath);
        FileProgressBar.PerformStep();
        foreach(AnimDef Def in AnimDefList)
        {
            Def.TransitionInfo = new hkbObject(modPrefix, -1, s_hkTransitionInfo);
            Def.TransitionEffect = new hkbObject(modPrefix, id, s_hkbBlendingTransitionEffect);
            Def.TransitionEffect.Params.Tag = Def.TransitionEffect.GetTag();
            Def.TransitionEffect.Params.variableBindingSet = null;
            Def.TransitionEffect.Params.name = Def.EventName + "Transition";
            Def.TransitionEffect.Params.duration = "0.600000";

            Def.TransitionInfo.Params.transition = Def.TransitionEffect.GetTag();
            Def.TransitionInfo.Params.eventId = Def.EventName;
            Def.TransitionInfo.Params.toStateId = Def.State.Params.stateID;
            Def.TransitionInfo.Params.toNestedStateId = 0;
            //Def.TransitionInfo.Params.toStateId = RootState.Params.stateID;
            //Def.TransitionInfo.Params.toNestedStateId = Def.State.Params.stateID;
            Def.TransitionInfo.Params.flags = nestedTransitionFlags;
            RootTransitionInfos.Add(Def.TransitionInfo.GetPatch());
            ExportPatch(Def.TransitionEffect, masterPath);
            id += 1;
            FileProgressBar.PerformStep();
        }
        FileProgressBar.PerformStep();
        RootTransitionInfoArray.Params.name = RootTransitionInfoArray.GetTag();
        RootTransitionInfoArray.Params.numElements = RootTransitionInfos.Count;
        RootTransitionInfoArray.Params.transitions = String.Join("\n", RootTransitionInfos);
        _2378.Params.Prefix = modPrefix;
        _2378.Params.transitions = String.Join("\n", RootTransitionInfos);


        ExportPatch(RootTransitionInfoArray, masterPath);
        _0106.Params.FirstPrefix = modPrefix;
        _0106.Params.EventNames = String.Join("\n", EventNames);
        _0106.Params.SecondPrefix = modPrefix;
        _0106.Params.VarNames = String.Join("\n", VarNames);

        _0107.Params.Prefix = modPrefix;

        _0108.Params.Prefix = modPrefix;
        _0108.Params.SecondPrefix = modPrefix;
        _0108.Params.EventFlags = String.Concat(Enumerable.Repeat(s_hkcFlags, EventNames.Count));

        _f0029.Params.AnimCount = 1656 + FileNames.Count;
        _m0029.Params.AnimCount = 1656 + FileNames.Count;

        _f0029.Params.Prefix = modPrefix;
        _m0029.Params.Prefix = modPrefix;

        List<string> mArr = new List<string>();
        List<string> fArr = new List<string>();
        for (int i = 0; i < SexExclusions.Count; i++)
        {
            string s = SexExclusions[i];
            Debug.WriteLine(s);
            switch (s)
            {
                
                case ("f"):
                    fArr.Add(FileNames[i]);
                    _m0029.Params.AnimCount -= 1;
                    break;
                case ("m"):
                    mArr.Add(FileNames[i]);
                    _f0029.Params.AnimCount -= 1;
                    break;
                default:
                    mArr.Add(FileNames[i]);
                    fArr.Add(FileNames[i]);
                    break;
            }
        }
        _f0029.Params.Anims = String.Join("\n", fArr);
        _m0029.Params.Anims = String.Join("\n", mArr);
        
        ExportPatch(_f0029, deffemalePath, "#0029");
        ExportPatch(_m0029, defmalePath, "#0029");
        ExportPatch(_0106, masterPath, "#0106");
        ExportPatch(_0107, masterPath, "#0107");
        ExportPatch(_0108, masterPath, "#0108");
        //ExportPatch(_2378, masterPath, "#2378"); temp

        
        StateMachine.Params.transitions = RootTransitionInfoArray.GetTag();
        RootState.Params.transitions = ExitTransitionInfoArray.GetTag();
        ExportPatch(RootState, masterPath);
        ExportPatch(StateMachine, masterPath);
        WritePatchMetadata();
        FileProgressBar.PerformStep();
        
        CultureInfo.CurrentCulture = Defculture;
        LaunchButton.Enabled = true;
    }

    private void fNISToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenTXTFile();
    }

    private void LaunchButton_Click(object sender, EventArgs e)
    {
        if (Enabled)
        {
            LaunchButton.Enabled = false;
            
            if (String.IsNullOrWhiteSpace(outputPath))
            {
                MessageBox.Show("Set an output path first.");
                return;
            }
            if (AnimDefList.Count == 0)
            {
                MessageBox.Show("Load an animlist first.");
                return;
            }
            if (String.IsNullOrWhiteSpace(ModAuthorInput.Text) || String.IsNullOrWhiteSpace(ModNameInput.Text) || (String.IsNullOrWhiteSpace(ModPrefixInput.Text)) || (String.IsNullOrWhiteSpace(ModLinkInput.Text)))
            {
                MessageBox.Show("All mod fields must be filled.");
                return;
            }
            modPrefix = ModPrefixInput.Text.Replace(" ", String.Empty).ToLower();
            modName = ModNameInput.Text.Replace(" ", String.Empty);

            ExportAnimlistToPatch();
        }
       
    }

    private void pathToolStripMenuItem_Click(object sender, EventArgs e)
    {
        SetOutputFolder();
    }
    private void ValidateKeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
        {
            e.Handled = true;
        }
        if (char.IsWhiteSpace(e.KeyChar) && sender == ModPrefixInput)
        {
            e.Handled = true;
        }
    }
    private void WritePatchMetadata()
    {
        string template =
@"name={0}
author={1}
site={2}
auto={3}
{4}";
        string args = "";
        if (ExportHiddenPatch)
        {
            args = "hidden=true";
        }
        System.IO.File.Move(filePath, filePath.Replace(filePath.Split("\\").Last(), "ATT_" + modName + "_" + "animlist.txt"));

        WriteToTXTFile(rootPath + "\\info.ini", String.Format(template, ModNameInput.Text, ModAuthorInput.Text, ModLinkInput.Text, "null", args));
    }
}

public class AnimDef
{
    public hkbObject State;
    public hkbObject Clip;
    public hkbObject BindingSet;
    public hkbObject TransitionInfo;
    public hkbObject TransitionEffect;
    public hkbObject ModifierRoot;
    public hkbObject Modifier;
    public hkbObject ModifierBindingSet;

    public string Type;
    public string EventName;
    public string File { get; set; }
    public string PlayMode;
    public List<string> Options;
    public string Sex;
    public List<string> AnimObjects;
    public AnimDef(string type, string eventname, string file, string playmode, List<string> modifiers, string sex, List<string> animObjects)
    {
        Type = type;
        EventName = eventname;
        File = file;
        PlayMode = playmode;
        Options = modifiers;

        Sex = sex;
        AnimObjects = animObjects;
    }

}
public class hkbObject
{
    private static string Root = "#{0}${1}";
    public virtual string PatchFormat { get; set; } = String.Empty;
    public int ID = -1;
    public string Prefix = String.Empty;

    public dynamic Params = new ExpandoObject();

    public hkbObject(string prefix, int id, string patchformat)
    {
        ID = id;
        Prefix = prefix;
        PatchFormat = patchformat;
    }
    public hkbObject(string prefix, string patchformat)
    {
        Prefix = prefix;
        PatchFormat = patchformat;
    }


    public string GetTag()
    {
        if (ID != -1)
        {
            return String.Format(Root, Prefix, ID);
        } else
        {
            return "null";
        }
    }
    public string[] GetParamText()
    {
        List<string> Values = new List<string>();
        foreach (KeyValuePair<string, object> kvp in Params)
        {
            if (kvp.Value != null)
            {
                Values.Add(kvp.Value.ToString());
            } else
            {
                Values.Add("null");
            }
        }
        return Values.ToArray();
    }
    public string GetPatch()
    {
        try
        {
            return String.Format(PatchFormat, GetParamText());
        }
        catch
        {
            return "null";
        }
    } 
}   


