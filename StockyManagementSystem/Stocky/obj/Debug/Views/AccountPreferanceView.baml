   M S B A M L   `   `   `  ����E  AStockyMain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Stocky.ViewModelsStocky.ViewModels  5      (    "Stocky.Views.AccountPreferanceView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -Lclr-namespace:Stocky.ViewModels     5      $	��600��5        � DataContext  5	      2   ,Stocky.ViewModels.AccountPreferanceViewModel  5
   
   .��5      � 
������� � 	Resources 5   
    �� 
TargetType �� Property �� Value��%�   '��       ']� x     �� 5      " ��6   .�5      ��  � ��$	 150��5      �� 5       � ��$ Right=�5      5      �� 5      " ]�6   .�5      ��  � ��$ 	0,20,20,0q�5      5      5       � RowDefinitions 5   
   �� 5      $	��200 �6   �� 5      $	��200 �6   �� 5      $	��200 �6   �� 5      $��30 �6   .+�5   
   �� $��15,10,15,10q�6   $
��0.60q�5       
�������._�5"      �� $��15,10,15,10q�6   .+�5#      �� $��Basic Details��6   $��Center=�5$      ]� 5%      .+�5&      �� $��
First Name��6   �� 5'      ��6   �� *userObj.FistName+5(      ]� 5)      .+�5*      �� $��	Last Name��6   �� 5+      ��6   �� *userObj.LastName+5,      ]� 5.      .+�5/      �� $��Initials��6   �� 50      ��6   �� *userObj.Initials+51      52      53      �� 54   
   $��1��6   $��15,10,15,10q�55      $
��0.60q�56      
�������._�58      �� $��1��6   $��15,10,15,10q�59      .+�5:      �� $��Account Details��6   $��Center=�5;      ]� 5<      .+�5=      �� $��	User Name��6   �� 5>      ��6   �� *userObj.UserName+5?      ]� 5@      .+�5A      �� $��Email��6   �� 5B      ��6   �� *userObj.Email+5C      5D      5E      �� 5F   
   $��2��6   $��15,10,15,10q�5G      $
��0.60q�5H      
�������._�5J      �� $��2��6   $��15,10,15,10q�5K      .+�5L      �� $��Password��6   $��Center=�5M      ]� 5N      .+�5O      �� $��Reset Password��6   �� 5P      -   5        $��resetpasswordcheckbox��5P      $��Center=�5Q      $
��Left=�5R       d� 	IsChecked 5S      �� *IsPasswordreset+5T      ]� 5U      .+�5V      �� $��Current Password��6   �� 5W      {�6   ��  �� ElementName$ resetpasswordcheckbox�� �� Path$ 	IsChecked�5X      ]� 5Y      .+�5Z      �� $��New Password��6   �� 5[      {�6   �� $ resetpasswordcheckbox��$ 	IsChecked���6e   �� *userObj.Password+5\      5]      5^      �� 5_   
   $��3��6   $��Apply CHnages��5`      ��5a      �� *Update+5b      5d      