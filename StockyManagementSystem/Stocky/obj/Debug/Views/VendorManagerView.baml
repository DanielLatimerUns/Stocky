   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5      R NWPFDebugger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=fb2ef86a2fb296fc8+clr-namespace:Debugger;assembly=WPFDebuggerDebugger 5      $    Stocky.Views.VendorManagerView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1Localclr-namespace:Stocky.ViewModels     5      :db+clr-namespace:Debugger;assembly=WPFDebugger     5        Debugger.ScreenDebugger    DebugObj$
  true��5	       � Triggers 5      :� 5   
    :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5      ��    OpenAnimation#��[  6   5       � DataContext 5      .   (Stocky.ViewModels.VendorManagerViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5   
   $��50 �6   �� 5      $��40 �6   �� 5   
   $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   
�������.+�5      � $��30��6   .+�5      �� $��	Vendor ID��6   �� 5      -   5        $��txtID��5      $	��150��6'   $��20��63   ��6?   �� *VendorOBJ.VendorID+5      � 5      $��30��6   .+�5      �� $
��Name��6   �� 5       -   5        $��txtTitle��5       $	��150��6*   $��20��66   ��6B   �� *VendorOBJ.VendorsName+5!      � 5"      $��30��6   5#      � 5$      $��30��6   $��10,13,10,10q�6$   .+�5%      �� -   5        $��	btnSeatch��5%      $��Search��6*   $��70��6;   ��6F   �� *UpdateVendoryList+5&      5'      �� 5(   
     ActionPanelBrush#���  6   $ 
Horizontal=�6E   $��	-4,45,4,5q�6^   $��2��6q   .+�5)      �� ��6   �� *	Buttons+��5*      �� 5+      .R�5,      �� $ 
Horizontal=�6%   5-      ��5/      �� 50      .R�51      �  � Orientation$ 
Horizontal=�6$   .+�52      ��   ToolBarButtons#���  6]   ��6%   �� *	Content+��6A   �� *	Command+6]   53      54      56      57      &   System.Windows.Controls.DataGrid  58   
   -   5        $��gdsales��58   8      
IsReadOnly .    RowHeaderWidth$ 0Q�6%   $��2��6G   	   AutoGenerateColumns	 .  ��6p   �� *
vendorList+
 �� UpdateSourceTrigger$
 PropertyChanged=� �� SelectedItem 6�   �� *SelectedVendorOBJ+   Columns 59      0  *System.Windows.Controls.DataGridTextColumn  5:      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *VendorsName+  5;      $ Description��6%    6:   �� *VendorsDescption+4  .System.Windows.Controls.DataGridCheckBoxColumn  5<      $ Online Vendor��6)    6@   �� *onlineVendor+ � ContextMenu 5>      �� 5?      .@�5@      m� .@�6   	Details6(   5A      5C      5D      5E      