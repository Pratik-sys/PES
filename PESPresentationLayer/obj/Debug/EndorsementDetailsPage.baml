   M S B A M L   `   `   `  ����O  KPESPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PESPresentationLayerPESPresentationLayer  5      1    +PESPresentationLayer.EndorsementDetailsPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PESPresentationLayer     5      -   5          N� 
Background  �#fff N� Title$ EndorsementDetailsPage��5
       � 	Resources 5         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 YellowButton �� 
TargetType	 �� 
TargetType
 ��Name �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%b  &	        &	 �     &	 �     &	 �     &	     &	 6    &	 Y    &	 |    &	 �    &		 �    &	
 �    7   7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FFDDDDDD��6;   �� 5   
   $ 	#FF707070��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6%   .�5      ��  �@�FocusVisualStyle8   61   �� 5        � ��8  6+   �� 5!       � ��8  6,   �� 5"       � ��# � ��6+   �� 5#       � ��$ 0q�60   �� 5$       � ��$ Center=�6;   �� 5%       � ��$ Center=�69   �� 5&       � ��$ 1q�6(   �� 5'       � �� 5(      �� 5)      "	 ��6&   .R�5*      �� $
 border��6!   $ 10��61   #��z��6C   #��z��6o   #��z��6�    . ._�5+      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5,       5-      P� 5.       �@��IsDefaulted . . �5/   "   �� $ border��6@   # � ��6T    � ��50      P� 51       �@J�IsMouseOver . . �52   "   �� $ border��6?    � �� �#EAB01E6S   �� 53   "   $ border��6@    � �� �#EAB01E6T   54      P� 55       � �� . . �56   "   �� $ border��6?    � �� �#F1CA686S   �� 57   "   $ border��6@    � �� �#F1CA686T   58      P� 59       � {� .  . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8   � ��6i   5=      5?      5A      5B      5C      .��5E      �� $��50q�6   $��Center=�6   .+�5F   
   �� -   5         ��Name$ txt_EndorsementHeading��5F      $��Center=�64   $��20q�6Q   $��Endorsement Details��6]   $��26(�6x   �� 5G   
   .+�5H      &   System.Windows.Controls.DataGrid  -   5          Name$ grid_endorsementDetails��5H      $	��300��68   "   VerticalScrollBarVisibility$
 Auto=�6E      AutoGenerateColumns .  $��16(�6�      Columns 5I      0  *System.Windows.Controls.DataGridTextColumn  5J      ,  &System.Windows.Controls.DataGridColumn   
IsReadOnly .    Header$ Endorsement Id��6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *endorsementId+6w     5K       . $ 	Policy Id��6\    6)   �� *policyNumber+6q     5L       . $ Field Changes��6\    6)   �� *FieldChanges+6u     5M       . $ Status��6a    6)   �� *endorsementStatus+6s   5O      5P      �� 5Q   
   -   5         ��Name$ btn_back��5Q      ���	#FF2F80ED���#2F80ED���#FFFFFF$��
Montserrat)�6f   $��10q�6~   $��5q�6�   $��Center=�6�   $	��100��6�   $
��Back��6�   $��16(�6�   #��� 
 6�   5R      5S      