﻿using P16_StepFunctions.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;

namespace P16_StepFunctions
{
    /// <summary>
    /// Class for the interaction logic of the window.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Field for the viewmodel.
        /// </summary>
        private MainWindowViewModel vm = new MainWindowViewModel();

        /// <summary>
        /// The constructor.
        /// </summary>
        public MainWindow()
        {
            // The window components are initialized.
            InitializeComponent();

            // DataContext gets the viewmodel.
            DataContext = vm;

            // Prepares some testdata for the datagrid.
            //vm.StepDataSource.Add(new StepData("<", 0, 0, new ArithmeticSignData("1", "<")));
            //vm.StepDataSource.Add(new StepData("<=", 0.1, 0.8, new ArithmeticSignData("2", "<=")));
            //vm.StepDataSource.Add(new StepData("<", 0.2, 1.2, new ArithmeticSignData("1", "<")));
            //vm.StepDataSource.Add(new StepData("<=", 0.3, 1.4, new ArithmeticSignData("2", "<=")));

            vm.StepDataSource.Add(new StepData("<", 0, 0, vm.LowerComparerItems.FirstOrDefault(x => x.ArithmeticSignKey == "1")));
            vm.StepDataSource.Add(new StepData("<=", 0.1, 0.8, vm.LowerComparerItems.FirstOrDefault(x => x.ArithmeticSignKey == "2")));
            vm.StepDataSource.Add(new StepData("<", 0.2, 1.2, vm.LowerComparerItems.FirstOrDefault(x => x.ArithmeticSignKey == "1")));
            vm.StepDataSource.Add(new StepData("<=", 0.3, 1.4, vm.LowerComparerItems.FirstOrDefault(x => x.ArithmeticSignKey == "2")));

            ChartDataRefresh();
        }

        /// <summary>
        /// As the window was loaded the cursor is set in the datagrid for adding stepfunction data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Sets the focus to the first cell of the gridrow for adding data.
            grd_stepdata_SetFocus();

            if (grd_stepdata.SelectionUnit == DataGridSelectionUnit.FullRow)
            {
                grd_stepdata.SelectedItems.Clear(); // Just work while the datagrid's SelectionUnit is "FullRow"
            }
        }

        /// <summary>
        /// Start adding a new item in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_stepdata_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            vm.IsInsertMode = true;
        }

        /// <summary>
        /// Start editing an existing item in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_stepdata_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            // Sets IsUpdateMode only if no insert action is running.
            // In case of an insert the events AddingNewItem and BeginningEdit are fired one by one.
            // In case of update only BeginningEdit is fired.
            if (!vm.IsInsertMode)
            {
                vm.IsUpdateMode = true;
            }
        }

        /// <summary>
        /// Analyzes the keydown in the datagrid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_stepdata_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // With the Enter-key the user can navigate in a gridrow from one cell to the next.
            // When the gridrow's final cell was reached, the cursor jumps to the next gridrow.
            var uiElement = e.OriginalSource as UIElement;
            int maxColIndex = grd_stepdata.Columns.Count - 1;

            if (e.Key == Key.Enter &&
                uiElement != null &&
                grd_stepdata.CurrentCell.Column.DisplayIndex < maxColIndex)
            {
                e.Handled = true;
                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }

            // The selected row will be deleted from the datagrid.
            if (e.Key == Key.Delete)
            {
                // Refreshes the chart control.
                ChartDataRefresh();
            }
        }

        /// <summary>
        /// When the user leaves a DataGrid-row after insert or update the chart shall be refreshed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_stepdata_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // If the CurrentCell-property of the datagrid was changed by the method grd_stepdata_SetFocus the RowEditEnding-event is fired one more time.
            // But this method shall not be executed twice, so it is interrupted here.
            if (vm.CurrentCellChanged)
            {
                return;
            }

            // Just executed when the EditAction is not Cancel.
            if (e.EditAction == DataGridEditAction.Cancel)
            {
                vm.IsInsertMode = false;
                vm.IsUpdateMode = false;
                return;
            }

            // Refreshes the chart control.
            ChartDataRefresh();

            // Clears the selected cells.
            if (grd_stepdata.SelectionUnit == DataGridSelectionUnit.Cell)
            {
                grd_stepdata.SelectedCells.Clear(); // Just works while the datagrid's SelectionUnit is "Cell"
            }

            // Clears the selected items (= rows).
            if (grd_stepdata.SelectionUnit == DataGridSelectionUnit.FullRow)
            {
                grd_stepdata.SelectedItems.Clear(); // Just works while the datagrid's SelectionUnit is "FullRow"
            }

            // Sets the focus to the first cell of the gridrow for adding data.
            grd_stepdata_SetFocus();
        }

        /// <summary>
        /// After editing of a cell the chart shall be refreshed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_stepdata_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!vm.IsInsertMode && vm.IsUpdateMode)
            {
                // Refreshes the chart control while update of an existing datagrid item.
                ChartDataRefresh();
            }
        }

        /// <summary>
        /// Sets the focus to the first cell of the gridrow for adding data.
        /// </summary>
        private void grd_stepdata_SetFocus()
        {
            int rowIndex = 0;

            // Sets the rowindex for insert.
            // To the first cell in the adding row.
            if (vm.IsInsertMode)
                rowIndex = grd_stepdata.Items.Count - 1;

            // Sets the rowindex for update.
            // Same cell but the next row.
            if (!vm.IsInsertMode && vm.IsUpdateMode)
                rowIndex = grd_stepdata.Items.IndexOf(grd_stepdata.CurrentItem);

            grd_stepdata.Focus();
            vm.CurrentCellChanged = true;
            grd_stepdata.CurrentCell = new DataGridCellInfo(grd_stepdata.Items[rowIndex], grd_stepdata.Columns[0]);
            vm.CurrentCellChanged = false;

            if (grd_stepdata.SelectionUnit == DataGridSelectionUnit.FullRow)
            {
                grd_stepdata.SelectedIndex = rowIndex; // Just work while the datagrid's SelectionUnit is "FullRow", not with "Cell"
            }

            vm.IsInsertMode = false;
            vm.IsUpdateMode = false;
        }

        /// <summary>
        /// Refreshes the chart control.
        /// </summary>
        private void ChartDataRefresh()
        {
            Chart myWinformChart = new Chart();
            myWinformChart.Dock = System.Windows.Forms.DockStyle.Fill;
            Series mySeries = new Series("series");
            mySeries.ChartType = SeriesChartType.Point;
            myWinformChart.Series.Add(mySeries);
            ChartArea myArea = new ChartArea();
            myWinformChart.ChartAreas.Add(myArea);
            myWinformChart.DataSource = vm.StepDataSource;
            myWinformChart.Series["series"].XValueMember = "LowerBound";
            myWinformChart.Series["series"].YValueMembers = "StepValue";
            host.Child = myWinformChart;
        }

    }
}
