   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5
      +clr-namespace:Stocky.ViewsStocky.Views  5      #    Stocky.Views.StockDetailsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      +Localclr-namespace:Stocky.ViewModels  5
      &Viewsclr-namespace:Stocky.Views  5      
�������.  (System.Windows.Interactivity.Interaction    Triggers  5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *Loaded+5       � DataContext 5      -   'Stocky.ViewModels.StockDetailsViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $	��450 �6   �� 5      $��30 �6   �� 5      $��* �6   .+�5   
   c� 
������� � 	Resources 5       �� 
TargetType �� Property	 �� Value��%X   '��       �� 5      " ��6   .�5       ��  � ��$	 20,0,5,0q�6/   5!      5"      .@�5#      �� $
��Save��6   ��6$   �� *UpdateStock+�� 5$      $��Close��6   ��6%   �� *UiCommands.CloseCurrentTab+�� 5%      $��	20,0,20,0q�6   �� 5&      $��Go to Purchase��6   $��Purchase��6Y   ��6.   �� *OpenTransactionDetails+6Y   �� 5'      $��
Go to Sale��6   $
��Sale��6U   ��6*   �� *OpenTransactionDetails+6U   �� 5(      $��	20,0,20,0q�6   �� 5)      $��Create Sale��6   ��6+   �� *UiCommands.LoadSaleTab+��6V   �� *
StockObj+�� 5*      $��Link to Purchase��6   ��60   �� *LinkPurchaseCommand+�� 5+      $��
Add Images��6   ��6*   �� *UploadPhotso+5,      *   $Stocky.Views.ListObjectSelectionView  5-   
   
   DisplayMember$
 Title��50   (   $��2��6>    K� ZIndex$	 999��6O      ObjectSourceList 5.   (   �� *ObjectSourceList+   SelectedObject 5/   (   �� *SelectedObject+y�51   (   �� *LinkVisibility+�� 53   
   
�������$��1��54      .+�55      �  � ColumnDefinitions 56      �� 57      $��2* �6'   �� 58      $	��10* �6'   �� 59      $	��300 �6'    5;      �� 5<      $��* �6$   �� 5=      $��50 �6$   .+�5?      �� $��	10,10,0,0q�6   $��2��60    5A      ��%Y  '��       ']� �     '�� �     �� 5B      " ��6    .�5C      ��  � ��$	 
60,0,150,0q�5D   %   �� 5E       � ��$	 Center=�5F   %   5G      �� 5H      " ]�6    .�5I      ��  � ��$	 0,20,0,0q�67   5J      �� 5K      " ��6    .�5L      ��  � ��$		 100��66   �� 5M       � ��$	 13(�69   5N      5O      .+�5P      ]� .+�5Q      �� $��Stock ID��6    �� 5R      $��50��6D   $
��Left=�6O   ��6"   �� *StockObj.Stockid+6O   5S      ]� 5T      .+�5U      �� $��Title��6    �� 5V      ��6"   �� *StockObj.Name+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .  5W      ]� 5X      .+�5Y      �� $��Description��6    �� 5Z       �� TextWrapping$
 Wrap=�6"    �� AcceptsReturn . $	��100��5]   "   $	��Top=�5^   "   ��5[   "   �� *StockObj.Description+$ PropertyChanged=� .  5^   "   5`      ]� 5a      .+�5b      �� $��Category��6    �� 5c      $	��240��6#   $��Right=�5d   %   $��	0,0,150,0q�5e   %    �� DisplayMemberPath$
 Name��5f   %    �� SelectedValuePath$ StockTypeID��5g   %   ��5e   8   �� *
StockTypes+ �� SelectedItem 5h   %   �� *StockObj.CategoryObject+5i      ]� 5j      .+�5k      �� $��
Value Band��6    �� 5l      $	��240��6#   $��Right=�5m   %   $��	0,0,150,0q�5n   %   $ Description��5q   #   ��5o   $   �� *
ValueBands+ 5p   $   �� *StockObj.ValueBandObject+5q   #   5s      ]� 5t      .+�5u      �� $��Created��6    (  "System.Windows.Controls.DatePicker  5v      $	��240��6%   $��Right=�5w   %   $��	0,0,150,0q�5x   %      SelectedDate 5y   %   �� *StockObj.Created+5{      ]� 5|      .+�5}      �� $��Updated��6      5~      $	��240��6%   $��Right=�5   %   $��	0,0,150,0q�5�   %    5�   %   �� *StockObj.Updated+5�      5�      �� 5�      
�������$��1q�5�      $��2��5�       �� CornerRadius$ 10��5�      $��
20,29,20,0q�5�      ._�5�      �� $	��350��6@   $	��240��6M   ��6   �� *	ImageLIst+��5�      �� 5�      .R�5�   "   �� $	��250��6;   $	��200��6G   ��6(   �� 6G   5�      5�      5�      5�      5�      �� 5�   
   $��2��6   $��30��5�      
�������$��20(�5�      $��15,0,0,0q�5�      Stock History5�      9� 5�   
   $��3��6   .��5�      �� ��6   �� *StockHistory+ �� View 5�      �� 5�      .P�5�      ��  �� Width$ 90��6)    �� DisplayMemberBinding 64   �� *Amount+.��5�      �� $��Amount��6(   $	��150��66   5�      �� 5�      $ 90��6)    64   �� *Status+.��5�      �� $��Status��6(   $	��250��66   5�      �� 5�      $ 90��6)    64   �� *
Submited+.��5�      �� $��Date Submitted��6(   $	��250��6>   5�      �� 5�      $ 90��6)    64   �� *
SubmitedBy+.��5�      �� $��Submitted By��6(   $	��250��6<   5�      5�      5�      5�      5�      5�      