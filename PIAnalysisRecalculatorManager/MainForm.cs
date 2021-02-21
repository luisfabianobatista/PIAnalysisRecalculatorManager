/****************************************************************************
*                    PI Analysis Recalculator Manager                       *
*              Copyright 2017 - Fabiano Batista, OSIsoft LLC                *
*                                                                           *
* Licensed under the Apache License, Version 2.0 (the "License");           *
* you may not use this file except in compliance with the License.          *
* You may obtain a copy of the License at                                   *
*                                                                           *
*                http://www.apache.org/licenses/LICENSE-2.0                 *
*                                                                           *
* Unless required by applicable law or agreed to in writing, software       *
* distributed under the License is distributed on an "AS IS" BASIS,         *
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  *
* See the License for the specific language governing permissions and       *
* limitations under the License.                                            *
*                                                                           *
/****************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PIAnalysisRecalculatorManager
{
   
    public partial class MainForm : Form
    {
        

        //Max number of threads for the delete output values bulk operation
        private const int c_AFThreadCount = 10;
        
        //Max number of characters allowed in the log viewer
        private const int c_imaxMsgBufferCapacity = 1000;


        //Initial max number of objects to be retrieved from a search
        int _iMaxRetrievedObjects = 1000000;

        //Enumerations
        public enum OpCode { search, backfill, recalculate, recalculate_legacy};

        //Global variables
        OSIsoft.AF.PISystem             _AFServer;
        OSIsoft.AF.AFDatabase           _AFDB;
        OSIsoft.AF.Asset.AFElement      _TargetElement;
        //bool                            _AttributeListFilterIsActive;
        Dictionary<string, bool>        _AttributeFilter;
        OSIsoft.AF.PI.PIServers         _PIServers;
        OSIsoft.AF.PI.PIServer          _PIServer;
        OSIsoft.AF.Analysis.AFAnalysisService _AnalysisService;
        OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> AnalysesAllElements;
        BindingSource _bindingSource = new BindingSource();
        DataTable _AnalysisDataTable;

        List<AFAnalysisObj> _AnalysisListFiltered = new List<AFAnalysisObj>();
 
        OpCode _opCode;

        System.Diagnostics.Stopwatch _OpDuration = new System.Diagnostics.Stopwatch();
        private Queue<string> _logMsgQueue = new Queue<string>(c_imaxMsgBufferCapacity);    
        
        public MainForm()
        {
            InitializeComponent();

            DialogResult myDlgResult;

            //Gets the AF database and other PI System objects
            try
            {
                _AFDB = OSIsoft.AF.UI.AFOperations.ConnectToDatabase(this, "", "", true, out myDlgResult);
                
                if (_AFDB !=null)
                {
                    _AFServer = _AFDB.PISystem;
                    afElementFindCtrl1.Database = _AFDB;

                    if (_AFServer != null)
                        _AnalysisService = _AFServer.AnalysisService;
                }
               

                              
            } catch (Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }

        /// <summary>
        /// Search all analyses associated with a given target element and with its child elements
        /// </summary>
        /// <param name="rootElement">The root AF element</param>
        /// <param name="includeChildElemnents">if set to true, the child elements will be included in the search</param>
        /// <param name="attrPathFilter">Filter by attribute name using wildcards (*, ?)</param>
        /// <param name="FilteredListAnalyses">The output list containing the analyses found</param>
        private void SearchAnalysesForRecalculation(OSIsoft.AF.Asset.AFElement rootElement,bool includeChildElemnents, string attrPathFilter, out
             OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> FilteredListAnalyses)
        {

            object objLock = new object(); //Used to present multiple access to the same list of analysis
        

            OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> filteredAnalysisList = 
                new OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis>();
            //Search values only for those attributes included by the attribute path filter
            // of if the filter is empty (not set)
            //This will already return everything in upper case and convert the wild cards expressions to regular expressions
            string attrNameFilterRegExpression = WildcardToRegex(attrPathFilter);

            OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Asset.AFElement> AnalysesElements;
            FilteredListAnalyses = new OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis>();

            if (includeChildElemnents) //Searchs for child elements
            {
                AnalysesElements = OSIsoft.AF.Asset.AFElement.FindElements(_AFDB, _TargetElement, "*", OSIsoft.AF.AFSearchField.Name, 
                    true, OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, _iMaxRetrievedObjects);

            }
            else //just creates an empty list
            {
                AnalysesElements = new OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Asset.AFElement>();
            }

            //Add the target element
            if (_TargetElement != null)
                AnalysesElements.Add(_TargetElement); //Including the root element to be processed too

            //Using parallelism to speed up the analyses search
            Parallel.ForEach(AnalysesElements, new ParallelOptions() { MaxDegreeOfParallelism = c_AFThreadCount }, (element, state) =>
            {

                if (backgroundWorker1.CancellationPending)
                {                    
                    state.Break(); //cancels the search
                   
                } else
                {
                    //Obtaining the list of analysis associated with the current element
                    OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses = element.GetAnalyses(OSIsoft.AF.AFSortField.Name, OSIsoft.AF.AFSortOrder.Ascending, element.Analyses.Count + 1);


                    if (attrPathFilter == String.Empty)
                    {
                        lock (objLock)
                        {
                            filteredAnalysisList.AddRange(elAnalyses);
                        }


                    }
                    else //if the filter is selected, all paths will have to be compared with the search string
                    {
                        OSIsoft.AF.AFCollectionList<OSIsoft.AF.Analysis.AFAnalysis> localFilteredAnalysisList =
                        new OSIsoft.AF.AFCollectionList<OSIsoft.AF.Analysis.AFAnalysis>();
                        //Adding all analysis to the group
                        foreach (var analysis in elAnalyses)
                        {

                            //Adding analysis which path matches the filter expression
                            if (System.Text.RegularExpressions.Regex.IsMatch(analysis.GetPath().ToUpper(), attrNameFilterRegExpression))
                            {
                                localFilteredAnalysisList.Add(analysis);

                            }


                        }

                        lock (objLock)
                        {
                            filteredAnalysisList.AddRange(localFilteredAnalysisList);
                        }


                    }
                }
                
                

            });

             
            FilteredListAnalyses = filteredAnalysisList;
               
        }


        private void LogMessage(string Message)
        {
          
            rtxtbMsgLog.AppendText(Message + "\n");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            

            try
            {
                List<object> args = (List<object>)e.Argument;
                OSIsoft.AF.Asset.AFValues afValues = new OSIsoft.AF.Asset.AFValues();

                OpCode opCode = ((OpCode)args[3]); //operation code (delete values, delete values + event frames, delete event frames, backfill (fill gaps) or recalculate)

                //Retrieving the list of analysis that are eligible for backfilling
                OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> analysesList =
                    (OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis>)args[5];

                if (opCode == OpCode.search) //Searching elements and their corresponding analyses
                {
                    backgroundWorker1.ReportProgress(0, "Searching analyses...\n");
                    OSIsoft.AF.Asset.AFElement elTargetElement = (OSIsoft.AF.Asset.AFElement)args[2]; //target AF element
                    string attrPathFilter = ((string)args[4]); //filter by attribute name using wildcards (*, ?)

                    SearchAnalysesForRecalculation(elTargetElement, ckbIncludeChildElements.Checked, attrPathFilter, out AnalysesAllElements);

                    if (AnalysesAllElements.Count==0)
                    {                
                        backgroundWorker1.ReportProgress(100, "No analysis was found for the current search criteria.");          
                    } else
                    {
                        backgroundWorker1.ReportProgress(100, AnalysesAllElements.Count + " analyses have been found.");
                    }


                }
                else if (analysesList != null) //either recalculating or backfilling (not searching) and at least one analysis selected
                {

                    OSIsoft.AF.Time.AFTimeRange timeRange;
                    //Parsing arguments from object sender
                    DateTime dtStartTime = (DateTime)args[0]; //start time
                    DateTime dtEndTime = (DateTime)args[1]; //end time
                    DateTime writeTime = dtStartTime;
                    bool abortedOperation = false;

                    object lockAbortedInfo = new object();

                    timeRange = new OSIsoft.AF.Time.AFTimeRange(dtStartTime, dtEndTime);


                    string nonCalcReason;
                    bool canCalculate;

                    canCalculate = _AnalysisService.CanQueueCalculation(out nonCalcReason);


                    Type myType = (typeof(OSIsoft.AF.Analysis.AFAnalysisService));

                  
                    if (canCalculate)
                    {

                        if (opCode == OpCode.recalculate) //recalculate (needs to delete values first - PI Server 2016 R2 will handle this)
                        {
                            if (chkbNewPIDataArchive.Checked) //The following will work if using PI Data Archive 2016 R2 or later for analysis output
                            {
                                object response = _AnalysisService.QueueCalculation(analysesList, timeRange, OSIsoft.AF.Analysis.AFAnalysisService.CalculationMode.DeleteExistingData);
                                backgroundWorker1.ReportProgress(100, analysesList.Count + " analyses have been queued for recalculation. Check the recalculation status using the PI System Explorer (Management plugin).");
                            }

                        
                        }
                        else if (opCode == OpCode.recalculate_legacy) //In this operation, the historical output values need to be explicitly deleted (old PI Data Archive version)
                        {




                            //Obtaining the list of attributes that are PI Point DRs associated with the analyses for deletion
                            List<OSIsoft.AF.Asset.AFAttribute> attrOutputs = GetAnalysesOutputPointDR(analysesList);

                            if (attrOutputs.Count > 0) //Are there PI Point data references to be processed?
                            {
                                backgroundWorker1.ReportProgress(0, "Explicitly deleting historical data of " + analysesList.Count.ToString() + " analyses. Please wait...");


                                //Using parallelism to speed up the deletes
                                Parallel.ForEach(attrOutputs, new ParallelOptions() { MaxDegreeOfParallelism = c_AFThreadCount }, (attr, state) =>
                                {

                                    if (backgroundWorker1.CancellationPending)
                                    {

                                        lock (lockAbortedInfo)
                                        {
                                            abortedOperation = true;

                                        }

                                        state.Stop(); //return without trying to queue the analyses

                                    }
                                    else
                                    {
                                        OSIsoft.AF.Asset.AFValues values = null;
                                        int valueCount = 0;

                                        values = attr.Data.RecordedValues(timeRange, OSIsoft.AF.Data.AFBoundaryType.Inside, null, null, true);
                                        valueCount = values.Count();

                                        if (valueCount > 0)
                                        {

                                            try
                                            {
                                                //This is the new method supported only in PI Data Archive 2015 and above (don't need to retrieve all values - just pass the range)
                                                //attr.Data.ReplaceValues(timeRange, new OSIsoft.AF.Asset.AFValues(), OSIsoft.AF.Data.AFBufferOption.BufferIfPossible);

                                                //This is the only method supported for PI Data Archive 2015 and below (need to retrieve all values before deleting it)
                                                attr.Data.UpdateValues(values, OSIsoft.AF.Data.AFUpdateOption.Remove, OSIsoft.AF.Data.AFBufferOption.BufferIfPossible);

                                                backgroundWorker1.ReportProgress(0, "Deleted " + valueCount.ToString() + " values from " + attr.GetPath() + ".");

                                            }
                                            catch (Exception ex1)
                                            {
                                                Console.WriteLine(ex1.Message + "\n");
                                                backgroundWorker1.ReportProgress(0, "Error deleting historical data from " + attr.GetPath() + ".");
                                            }

                                        }
                                    }

                                });

                                //Filling gaps, because, at this point, the output values have already been deleted

                                if (!abortedOperation)
                                {
                                    DialogResult dr = MessageBox.Show("Before proceeding, follow the steps below:\n   - If buffer is enabled, wait until all delete events are flushed to the PI Data Archive.\n" +
                                        "   - If the PI Data Archive is in a PI collective AND the buffer is disabled, do not proceed - switch collective member and re-run this operation for each one of the collective members.\nProceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    _AnalysisService.QueueCalculation(analysesList, timeRange, OSIsoft.AF.Analysis.AFAnalysisService.CalculationMode.FillDataGaps);

                                    if (dr == DialogResult.Yes)
                                    {
                                        backgroundWorker1.ReportProgress(100, analysesList.Count + " analyses have been queued for backfilling. You can check the backfilling status by using the PI System Explorer (Management plugin).");
                                    }
                                    else
                                    {
                                        backgroundWorker1.ReportProgress(100, "The selected analysis were not queued for backfilling. Make sure to the delete the analyses output values in all collective members before proceeding with backfilling.");
                                    }

                                }
                                else
                                {
                                    backgroundWorker1.ReportProgress(100, "The data deletion operation was aborted.");
                                    return; // terminates the current activity
                                }

                            }


                        }
                        else //Fill gaps only (no values had to be deleted)
                        {


                            object response = _AnalysisService.QueueCalculation(analysesList, timeRange, OSIsoft.AF.Analysis.AFAnalysisService.CalculationMode.FillDataGaps);
                            backgroundWorker1.ReportProgress(100, analysesList.Count + " analyses have been queued for backfilling. You can check the backfilling status by using the PI System Explorer (Management plugin).");
                                                                                   
                        }

                    } else
                    {
                        backgroundWorker1.ReportProgress(100, analysesList.Count + " analyses can't be queued for recalculation. " + nonCalcReason);
                    }



                   


                }
                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            progressBar1.Visible = false;

            _OpDuration.Stop();

            lblMessage.Text= String.Empty;


            //Only refresh the datagridview if the the opCode = search
            if (_opCode ==OpCode.search)
            {
                //_bindingSource.Clear();
                
                _AnalysisListFiltered.Clear();
                _bindingSource.DataSource = null;
               
                foreach (var analysis in AnalysesAllElements)
                {

                    _AnalysisListFiltered.Add(new AFAnalysisObj(analysis, false));

                }

                _AnalysisDataTable = _AnalysisListFiltered.ToDataTable();


                //Adding the list of analysis results in the path order and binding it to the datagridview control

                _bindingSource.DataSource = _AnalysisDataTable;

                advancedDataGridView1.DataSource = _bindingSource;

                //Hiding first column
                advancedDataGridView1.Columns[0].Visible = false;

                //Allowing users to check the analysis "Select" cell

                advancedDataGridView1.Columns[1].ReadOnly = false;
                for (int i=2; i<advancedDataGridView1.Columns.Count; i++)
                {
                    advancedDataGridView1.Columns[i].ReadOnly = true;
                }
                


                advancedDataGridView1.Refresh();
               
            }
           

            //Disabling "Abort" button to make it available only when the operation requires explit tag values deletion
            btnAbort.Enabled = false;
      
            rtxtbMsgLog.AppendText("Operation duration: " + _OpDuration.Elapsed.TotalMinutes.ToString("0.000") + " minutes.\n");
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {

                backgroundWorker1.CancelAsync();
                rtxtbMsgLog.AppendText("Aborting operation...\n");
                
            }

            
        }

        private void btnRunOperation_Click(object sender, EventArgs e)
        {
            //Always clears the message log window first
            rtxtbMsgLog.Clear();

            if (_AFServer!=null)
            {

                    
                if (rbtnBackfill.Checked)
                {
                        
                    ProcessData(OpCode.backfill);
                }
                else if (rbtnRecalculate.Checked)
                {
                        

                    if (chkbNewPIDataArchive.Checked)
                    {
                           
                        ProcessData(OpCode.recalculate);
                    }
                    else
                    {
                        //The "Abort" button will be available when PI data values need to be explicitly deleted
                        //(which would be a very time consuming operation)
                        btnAbort.Enabled = true;

                        ProcessData(OpCode.recalculate_legacy);
                    }                        
                }
                else //Delete pi tag values only - there was a 3rd radio button with the option "Delete Values Only", but it was removed for simplicity
                {
                    //btnAbort.Enabled = true;

                    //ProcessData(OpCode.deleteTagValues);
                }

               
            } else
            {

                MessageBox.Show("Connect to the PI AF Server first.", "Not connected to AF", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }
        
  
        /// <summary>
        /// Reports progress changes from the Background Worker to the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            var progressMsg = e.UserState as string;
            LogMessage(progressMsg);
        }

       
        
        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                
                numUpDownMaxSearchResults.Value = _iMaxRetrievedObjects;
               
                //Setting attribute filter initial state
                _AttributeFilter = new Dictionary<string, bool>();

                //The following 2 rows are being used to improve datagridview performance
                //Reference: http://stackoverflow.com/questions/10226992/slow-performance-in-populating-datagridview-with-large-data
                advancedDataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing; //or even better .DisableResizing. Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
                advancedDataGridView1.RowHeadersVisible = false;

                //Allowing to choose the proper PI Server
                _PIServers = new OSIsoft.AF.PI.PIServers();

                //Tries to connect to the primary first
                _PIServer = _PIServers.DefaultPIServer;
                

                this.Text = _AFDB.GetPath() + " - PI Analysis Recalculator Manager";
            }
            catch
            {
                MessageBox.Show("Error connecting to the PI System", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
  
        }


        
        /// <summary>
        /// Helper method to encapsulate the parameters for the background worker
        /// </summary>
        /// <param name="opCode">Operation code</param>
        private void ProcessData(OpCode opCode)
        {
            
            string reason;

            //Always checks if the connection is OK before trying to execute an operation
            if (_AFServer.ConnectionInfo.IsConnected)
            {
                //It will only do anything if the current analysis service instance support calculation queueing
                if (_AnalysisService.CanQueueCalculation(out reason))
                {
                    _opCode = opCode;

                    List<object> bWorkArguments = new List<object>();

                    OSIsoft.AF.Time.AFTime st, et;

                    if (backgroundWorker1.IsBusy)
                    {
                        MessageBox.Show("Please wait the current operation to be completed or cancel it first.");

                    }
                    else
                    {
                        //Starts timer used to report the operation total duration
                        _OpDuration.Restart();

                        if (opCode == OpCode.search) //Search Analyses
                        {

                            //Build object to pass to background worker
                            bWorkArguments.Add((DateTime)System.DateTime.Now);
                            bWorkArguments.Add((DateTime)System.DateTime.Now);
                            bWorkArguments.Add(_TargetElement);
                            bWorkArguments.Add(0);
                            bWorkArguments.Add(txtAttrFilter.Text); //The attr filter field
                            bWorkArguments.Add(AnalysesAllElements);

                            progressBar1.Visible = true;
                            progressBar1.Style = ProgressBarStyle.Marquee;
                            progressBar1.MarqueeAnimationSpeed = 30;
                            backgroundWorker1.RunWorkerAsync(bWorkArguments);

                        }
                        else //all other operations
                        {

                            //List that will hold the selected analyses
                            OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> selectedAnalyses =
                                new OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis>();

                            int iNotIncluded = 0;
                            //Getting all selected elements from binding source with enabled status (disabled will be ignored!)


                            for (int i = 0; i < _bindingSource.Count; i++)
                            {
                                DataRowView dtRowView = (DataRowView) _bindingSource[i];

                                //ItemArray[1] is "Select" and ItemArray[9] is "Status"
                                if ((bool)dtRowView.Row.ItemArray[1])
                                {

                                    if ((String)dtRowView.Row.ItemArray[9] == "Enabled")
                                    {
                                        selectedAnalyses.Add((OSIsoft.AF.Analysis.AFAnalysis)dtRowView.Row.ItemArray[0]);
                                    } else
                                    {
                                        iNotIncluded++;
                                    }
                                    
                                }
                                                                
                            }
                            
                           
                            if (iNotIncluded > 0)
                            {
                                LogMessage(iNotIncluded.ToString() + " analyses will not be included because their are in error or not enabled.");

                            }

                            //First checks if time range fields are valid and not empty
                            if (selectedAnalyses.Count > 0)
                            {


                                if (afDtPickerStartTime.Text != String.Empty && afDtPickerEndTime.Text != String.Empty && OSIsoft.AF.Time.AFTime.TryParse(afDtPickerStartTime.Text, out st)
                                && OSIsoft.AF.Time.AFTime.TryParse(afDtPickerEndTime.Text, out et))
                                {
                                    //Build object to pass to background worker
                                    bWorkArguments.Add((DateTime)st);
                                    bWorkArguments.Add((DateTime)et);
                                    bWorkArguments.Add(_TargetElement);
                                    bWorkArguments.Add(opCode);
                                    bWorkArguments.Add(txtAttrFilter.Text); //The attr filter field
                                    bWorkArguments.Add(selectedAnalyses);



                                    if (opCode != OpCode.backfill) //recalculation, recalculation legacy and pi point data delete operations
                                    {
                                        DialogResult dgResult = DialogResult.Cancel;
                                        string strLogMsg = String.Empty;


                                        switch (opCode)
                                        {
                                            case OpCode.recalculate_legacy:
                                                dgResult = MessageBox.Show("PI Data is going to be deleted. " +
                                                    "To continue, make sure the PI Buffer Subsystem has been previously configured" +
                                                    " (only applicable is the analyses outputs are being stored in a PI collective). " +
                                                    "\nIf buffer is not enabled, you will need to execute this operation multiple times " +
                                                    "to make sure the deletes are replicated to all collective members. \nProceed?",
                                            "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                                strLogMsg = "Started the recalculation operation for the time range between " +
                                                    st.LocalTime.ToString() +
                                                    " and " + et.LocalTime.ToString() + "...";
                                                break;
                                            case OpCode.recalculate:
                                                dgResult = MessageBox.Show("PI Data is going to be deleted. Proceed?",
                                            "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                                strLogMsg = "Started the recalculation operation for the time range between " +
                                                    st.LocalTime.ToString() +
                                                    " and " + et.LocalTime.ToString() + "...";
                                                break;
                                            default:
                                                break;

                                        }


                                        if (dgResult == DialogResult.Yes)
                                        {
                                            LogMessage(strLogMsg);
                                            progressBar1.Visible = true;
                                            progressBar1.Style = ProgressBarStyle.Marquee;
                                            //progressBar1.Style = ProgressBarStyle.Blocks;
                                            progressBar1.MarqueeAnimationSpeed = 30;

                                            backgroundWorker1.RunWorkerAsync(bWorkArguments);
                                        }
                                        else
                                        {
                                            LogMessage("Operation cancelled.");
                                        }
                                    }
                                    else
                                    {
                                        LogMessage("Started the backfilling operation for the time range between " + st.LocalTime.ToString() + " and " + et.LocalTime.ToString() + "...");
                                        backgroundWorker1.RunWorkerAsync(bWorkArguments);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter the time range data in the PI time format and try again.", "Invalid time range", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }

                            }
                            else
                            {
                                MessageBox.Show("You must select a running analysis first.", "No running analysis selected", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                        }

                    }
                }
                else
                {
                    MessageBox.Show(reason, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else
            {
                MessageBox.Show("Connection failure detected. Please check the network or status of the AF Server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


     

        }


        /// <summary>
        /// Gets the list of attributes used as PI Point DR outputs from all the analyses of a given element
        /// </summary>
        /// <param name="afelement">The input element</param>
        /// <returns></returns>
        private List<OSIsoft.AF.Asset.AFAttribute> GetAnalysesOutputPointDR(OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses)
        {
            object lockObj = new object();

            backgroundWorker1.ReportProgress(100, "Searching PI Point data references for explict data deletion and analyses recalculation. Please wait...");

            //This list will hold all attributes used as Analyses outputs
            var analysisOutputsPIPoint = new List<OSIsoft.AF.Asset.AFAttribute>();

            List<OSIsoft.AF.Asset.AFAttribute> attrListAnalysesFull = new List<OSIsoft.AF.Asset.AFAttribute>();
            
            Parallel.ForEach(elAnalyses, new ParallelOptions() { MaxDegreeOfParallelism = c_AFThreadCount }, (analysis, state) => 
            {
                if (backgroundWorker1.CancellationPending)
                {
                    //debug
                    backgroundWorker1.ReportProgress(100, "The recalculation operation has been aborted.");

                    analysisOutputsPIPoint.Clear();
                    state.Stop(); //return without trying to queue the analyses
                 }

                List<OSIsoft.AF.Asset.AFAttribute> attrListAnalysesPartial = new List<OSIsoft.AF.Asset.AFAttribute>();

                var configuration = analysis.AnalysisRule.GetConfiguration();
                

                foreach (var output in configuration.ResolvedOutputs)
                {


                    var attr = (OSIsoft.AF.Asset.AFAttribute)output.Attribute;


                        //Need to do all the checks first before trying to find if it's associated with a PI Point
                    if (attr != null)
                    {

                            attrListAnalysesPartial.Add(attr);

                    }



                }

                lock (lockObj)
                {
                    attrListAnalysesFull.AddRange(attrListAnalysesPartial);
                }
            });

            if (attrListAnalysesFull.Count>0)
            {
                analysisOutputsPIPoint = attrListAnalysesFull.GetValidPIPointAttributes().ToList();

            }

            int pointCount = analysisOutputsPIPoint.Count;


            if (pointCount==0)
            {
                backgroundWorker1.ReportProgress(100, "The selected analyses do not contain PI Point data references for explict data deletion. No recalculation will be performed.");
            } else
            {

                backgroundWorker1.ReportProgress(100, pointCount.ToString() + " PI Points have been found for explict data deletion.");
            }
            return analysisOutputsPIPoint;

        }

        private OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.EventFrame.AFEventFrame> 
            GetEFAnalyses(OSIsoft.AF.AFNamedCollectionList<OSIsoft.AF.Analysis.AFAnalysis> elAnalyses)
        {

            return null;
        }

        private void btnPISystem_Click(object sender, EventArgs e)
        {

        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            OSIsoft.AF.AFDatabase dbSelected = OSIsoft.AF.UI.AFOperations.SelectDatabase(this, _AFServer, _AFDB);

            if (dbSelected == null)
            {
                MessageBox.Show("Database was not changed",  "Information",MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                if (_AFDB!=dbSelected)
                {
                    //Clears analysis list and filters
                    ResetAnalysesList();
                }

                _AFDB = dbSelected;
                _AFServer = _AFDB.PISystem;

                afElementFindCtrl1.Text = "";
                _TargetElement = null;
                afElementFindCtrl1.Database = _AFDB;
                
                this.Text = _AFDB.GetPath() + " - PI Analysis Recalculation Manager";

                _AnalysisService = _AFServer.AnalysisService;

                

            }
            
        }

        private void numUpDownMaxSearchResults_ValueChanged(object sender, EventArgs e)
        {
            _iMaxRetrievedObjects = (int) numUpDownMaxSearchResults.Value;
        }



        /// <summary>
        /// Resets attribute filter to initial state (empty) and disable attribute filter check and clears datagridview
        /// </summary>
        private void ResetAnalysesList()
        {

            //_AttributeListFilterIsActive = false;
            _AttributeFilter.Clear();


            //Disable single attribute filter textbox

            txtAttrFilter.Clear();
            txtAttrFilter.Enabled = true;


            if(_AnalysisDataTable !=null)
            {
                _AnalysisDataTable.Clear();
            }
            
            if(_AnalysisListFiltered !=null)
            {
                _AnalysisListFiltered.Clear();
            }
            


        }

        private void afElementFindCtrl1_AFElementUpdated(object sender, CancelEventArgs e)
        {
            _TargetElement = afElementFindCtrl1.AFElement;

            //Clears analysis list and filters
            ResetAnalysesList();

        }


        /// <summary>
        /// Allowing the user to select other members of the PI Server collective
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPIDataArchives_Click(object sender, EventArgs e)
        {
            OSIsoft.AF.UI.AFOperations.ShowPIServers(this, _PIServer, false);
        }

        public static string WildcardToRegex(string pattern)
        {
            return "^" + System.Text.RegularExpressions.Regex.Escape(pattern.ToUpper())
                              .Replace(@"\*", ".*")
                              .Replace(@"\?", ".")
                       + "$";
        }

        private void rbtnTagValuesOnly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchAnalysis_Click(object sender, EventArgs e)
        {
            if (_AFDB != null && _AnalysisService != null && _AFServer !=null)
            {
                //Making sure it gets the most recent db state
                _AFDB.Refresh();
                
                //Always clears the message log window when there is a new search
                rtxtbMsgLog.Clear();

                if (_bindingSource.Count > 0)
                {
                    DialogResult dlResult = MessageBox.Show("The current search result list will be overwritten. Proceed?", "Overwriting results", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlResult == DialogResult.Yes)
                    {
                        //_bindingSource.Clear();
                        _AnalysisDataTable.Clear();

                        btnAbort.Enabled = true;
                        ProcessData(OpCode.search);

                    }
                }
                else
                {
                    btnAbort.Enabled = true;
                    ProcessData(OpCode.search);
                }
            } else
            {
                MessageBox.Show("Not connected with to the PI AF Server. Check network status and/or AF database permissions.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCheckAllRows_Click(object sender, EventArgs e)
        {


            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in advancedDataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                chk.Value = chk.Value == null ? true : true; //because chk.Value is initialy null
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnUncheckAllRows_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in advancedDataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                chk.Value = chk.Value == null ? false : false; //because chk.Value is initialy null
            }
        }


        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

            advancedDataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing; //or even better .DisableResizing. Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

        }


        private void afDtPickerStartTime_Validated(object sender, EventArgs e)
        {
            if (afDtPickerStartTime.Text != String.Empty)
            {
                OSIsoft.AF.Time.AFTime result;
                bool ParseOk = OSIsoft.AF.Time.AFTime.TryParse(afDtPickerStartTime.Text, out result);
                
                if (!ParseOk)
                {
                    errorProvider1.SetError(afDtPickerStartTime, "Wrong PI Time format");
                } else
                {
                    errorProvider1.Clear();
                }

            }
                                 
        }

        private void afDtPickerEndTime_Validated(object sender, EventArgs e)
        {
            if (afDtPickerEndTime.Text != String.Empty)
            {
                OSIsoft.AF.Time.AFTime result;
                bool ParseOk = OSIsoft.AF.Time.AFTime.TryParse(afDtPickerEndTime.Text, out result);

                if (!ParseOk)
                {
                    errorProvider2.SetError(afDtPickerEndTime, "Wrong PI Time format");
                }
                else
                {
                    errorProvider2.Clear();
                }

            }
        }

        private void rbtnRecalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRecalculate.Checked)
            {
                chkbNewPIDataArchive.Enabled = true;
            }
            else
            {
                chkbNewPIDataArchive.Checked = false;
                chkbNewPIDataArchive.Enabled = false;
            }
        }

        private void btnEnableSelectedAnalyses_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            bool noItemChecked;

            int iNotEnabled = ChangeSelectedAnalysesStatus(OSIsoft.AF.Analysis.AFStatus.Enabled, true, out noItemChecked);

            if (noItemChecked)
            {
                MessageBox.Show("Select at least one analysis ", "No analysis selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor.Current = Cursors.Default;
            if (iNotEnabled>0)
            MessageBox.Show(iNotEnabled.ToString() + " analyses cannot be enabled, because they are either in error state or not ready.", "Items not included", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDisableSelectedAnalyses_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            bool noItemChecked;

            ChangeSelectedAnalysesStatus(OSIsoft.AF.Analysis.AFStatus.Disabled, false, out noItemChecked);

            if (noItemChecked)
            {
                MessageBox.Show("Select at least one analysis ", "No analysis selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Cursor.Current = Cursors.Default;         

        }


        private void btnRefreshStatusAnalyses_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;


            RefreshAnalysesStatus();

            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Changes the state of a list of analyses
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="includeDisabledStatusOnly"></param>
        /// <param name="noItemSelected"></param>
        /// <returns></returns>
        private int ChangeSelectedAnalysesStatus(OSIsoft.AF.Analysis.AFStatus newStatus, bool includeDisabledStatusOnly, out bool noItemSelected)
        {

            int iCounter = 0;
            bool bolNoItemSelected = true;
            List<OSIsoft.AF.Analysis.AFAnalysis> analyses = new List<OSIsoft.AF.Analysis.AFAnalysis>();

            for (int i = 0; i < _bindingSource.Count; i++)
            {
                DataRowView dtRowView = (DataRowView)_bindingSource[i];

                //ItemArray[1] is "Select"
                if ((bool)dtRowView.Row.ItemArray[1])
                {
                    bolNoItemSelected = false;
                    string analysisCurrentStatus = (String)dtRowView.Row.ItemArray[9];

                    if (!includeDisabledStatusOnly || analysisCurrentStatus == "Disabled")
                    {
                        analyses.Add((OSIsoft.AF.Analysis.AFAnalysis)dtRowView.Row.ItemArray[0]);

                    } 
                    
                }

            }

         
            if (analyses.Count > 0)
            {

                OSIsoft.AF.Analysis.AFAnalysis.SetStatus(analyses, newStatus);

            }

            //Refreshing the binding source with updates of the analysis status

            RefreshAnalysesStatus();

            _bindingSource.ResetBindings(false);
            
            noItemSelected = bolNoItemSelected;
            return iCounter;
        }



        private void btnCheckSelectedRows_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in advancedDataGridView1.SelectedRows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                chk.Value = chk.Value == null ? true : true; //because chk.Value is initialy null
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnUncheckSelectedRows_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (DataGridViewRow row in advancedDataGridView1.SelectedRows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells["Select"];
                chk.Value = chk.Value == null ? false : false; //because chk.Value is initialy null
            }

            Cursor.Current = Cursors.Default;

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutbox = new AboutBox();
            aboutbox.Show();
        }

        private void usersGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string appStartupPath = System.IO.Directory.GetCurrentDirectory();
                string manualPathFile = appStartupPath + @"\Documentation\PI Analyses recalculator manager - User's Guide.htm";
                System.Diagnostics.Process.Start(manualPathFile);
            }
            catch
            {
                MessageBox.Show("Error opening the documentation file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                        
            //
            //Process process = new Process();
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //process.StartInfo = startInfo;

            //startInfo.FileName = manualPathFile;
            //process.Start();
            //
        }

        private void advancedDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void advancedDataGridView1_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            this._bindingSource.Sort = this.advancedDataGridView1.SortString;

          
                       
            
        }

        private void advancedDataGridView1_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
           
            this._bindingSource.Filter = this.advancedDataGridView1.FilterString;
                      
        }

        private void advancedDataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            advancedDataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing; //or even better .DisableResizing. Most time consumption enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

        }

        private void RefreshAnalysesStatus()
        {
            foreach (DataRow row in _AnalysisDataTable.Rows)
            {
                OSIsoft.AF.Analysis.AFAnalysis analysis = (OSIsoft.AF.Analysis.AFAnalysis)row.ItemArray[0];

                //colum index 9 contains the status info
                row[9] = analysis.Status.ToString();
            }

            //Refresh binding with updated collection data
            _bindingSource.ResetBindings(false);

        }
    }
}
