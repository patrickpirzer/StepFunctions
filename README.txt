Hosting a Windows Form Control in WPF

1. Add references to the project:
   - WindowsFormsIntegration
   - System.Windows.Forms
   - System.Windows.Forms.DataVisualization
2. Add in the XAML the following namespace mappings:
   xmlns:wf="clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms"
   xmlns:winformchart="clr-namespace:System.Windows.Forms.DataVisualization.Charting;assembly=System.Windows.Forms.DataVisualization"
3. Add the chart control in XAML by:
   <WindowsFormsHost Grid.Row="0"
                     Grid.Column="1"
                     Margin="5">
       <winformchart:Chart></winformchart:Chart>
   </WindowsFormsHost>


Sources:
http://stackoverflow.com/questions/32959863/how-to-add-net-chart-component-in-wpf (Important)
https://msdn.microsoft.com/en-us/library/ms751761(v=vs.100).aspx
https://msdn.microsoft.com/en-us/library/ms742875(v=vs.100).aspx
http://stackoverflow.com/questions/2377862/embedding-winforms-graph-in-wpf-window (Important)
http://stackoverflow.com/questions/41679342/can-an-embedded-winform-chart-be-bound-to-an-observable-collection-in-wpf (Very important)
