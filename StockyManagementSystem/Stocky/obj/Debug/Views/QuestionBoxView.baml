   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Stocky.ViewsStocky.Views  5   	   5clr-namespace:Stocky.UI.DialogsStocky.UI.Dialogs  5   	   "    Stocky.Views.QuestionBoxView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Stocky.Views     5   	   .VMclr-namespace:Stocky.UI.Dialogs     5   	   $	��300��5	   !   $	��400��6.     � WindowStartupLocation$  CenterOwner=�5
   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	   $��1q�5   	   
����ح� � Title 5	   	   �� *Title+ � DataContext 5      #   Stocky.UI.Dialogs.QeustionBox  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� $��Center=�6   .+�5      �� $��Center=�5       �� TextWrapping$
 Wrap=�5      $	��350��5      $	��100��5      $��Center=�5      $��Center=�5      $��0q�5      $��13(�5       ��5      �� *
Question+5       �� 5"      $
 Wrap=�5#      $	��100��5$      $	��350��5%      $��Center=�5&      $��Center=�5'      $��0,0,0,0q�5(      ��5"      �� *Answer+ �� UpdateSourceTrigger$ 	LostFocus=�5(      5)      � 5*   
   $��1��6   $��Center=�6!   .+�5+      �� -   5        $	��200��5+      $��30��5,      $��	0,-10,0,0q�5-      $��Submit��5.      6&   50      51      52      