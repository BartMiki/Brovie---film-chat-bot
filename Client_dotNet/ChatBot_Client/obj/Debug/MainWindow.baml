   M S B A M L   `   `   `  ����I  EChatBot_Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ChatBot_ClientChatBot_Client  5   	       ChatBot_Client.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ChatBot_Client     5   	   -   5          � Title$  ChatBot��5   	   $	��550��6   $	��350��6&    � 	Resources 5
       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $+ %WpfResources/ChatMessageTemplate.xaml?�6%   �� 5      $# WpfResources/OtherStyles.xaml?�6%   %    5      .��5      ]� ��. .+�5   
   o� $	��Top=�6   .@�5      m� $��Addons��6   .@�5      m� -   5        $��Connection Tester��5      66   5      m� 5      $��About��6   .@�5      m� $��About��6   5      5      �� 5   
   $��Bottom=�6      StackPanelStyle#��[  6-   .+�5      ]� $��2q�6   .+�5      �� -   5        $	��5,0q�5      $��40��5      $��Right=�5!        SendButtonTemplate#��[ 5"      
�������$
��Bold"�5$      $��Center=�5%      $��Center=�5&      $��Center=�5'      $��Center=�5(      ��5       ��  �� RelativeSource �� *Self+ �� Path$ Width�.��5*      Send��5+      �� 5,       �� 
TargetType" ��6    .�5-      ��  �� Property � �� �� Value
 ��� �6;   	 �� Triggers	 5.      P� 5/   "   
 P� Property
 �@J�IsMouseOver P� Value . . �50   &   ��  � ��
 ����6C   51   #   53      55      �� 56       �� Padding$ 0q�57      $��3q�58       �� CornerRadius$ 7��59      ��5:      �� 5;      " ��6    .�5<      ��  � ��
 ��� �6R   	 5=      P� 5>   "   
 �@J�IsMouseOver . . �5?   &   ��  � ��
 ����6Y   5@   #   5B      ._�5D      �� -   5         ��Name$ 
MessageBox��5E      
������ $��Center=�5H      $��20(�5I      
������ ��5G      ��  �� *FindAncestor+ �� AncestorType$ BorderL�$ Height�.��5J   2   5K      5L      5M      5N      � 5O   
     	GridStyle#��[ 6   .+�5P      �� $ 3q�6   
����� �$��3q�6;   $ 7��6O   ._�5Q      �� -   5         ��Name$ MessagesListBox��5S      $��Disabled=�5R      
������   MessageTemplate#��[ 5V      $��Stretch=�5W      ��5U      �� 5X      5Y      5Z      5[      5\      