   M S B A M L   `   `   `  ����P  LPopulationProtocolApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:PopulationProtocolAppPopulationProtocolApp  5   	   '    !PopulationProtocolApp.WindowGraph   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:PopulationProtocolApp     5   	   -   5          � Title$  WindowGraph��5   	   $��360.16��6   $��332.888��6-   .��5	      � ���	#FFE5E5E6$
��Left=�6"   $	��323��6=   $��0,0,0,1q�6I    � RowDefinitions 5
   
   �� 5      $	��81* �6   �� 5      $��0* �6   �� 5      $��2* �6   .+�5   
   � $
��Left=�6   $	��269��6*   $��	10,10,0,0q�67   $	��Top=�6J   $	��298��6b   .+�5      �� -   5         ��Name$ labNumberOfNodes��5      $��Number of nodes: ��6.   $
��Left=�6J   $��6,35,0,0q�6e   $	��Top=�6w   $	��107��6�   �� 5      -   5         ��Name$ txtBxNumberOfNodes��5      $
��Left=�62   $��23��6M   $��
122,38,0,0q�6Y    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $��80��6�   6�   �� 5      -   5         ��Name$ groupBxDefineGraph��5      $��Define graph��63   $
��Left=�6I   $	��189��6d   $��0,80,0,0q�6q   $	��Top=�6�   $	��298��6�   {�. .��5      � $	��169��6   $��	0,0,-2,-2q�6$   $	��Top=�67   .+�5      �� $��symbol:��6   $
��Left=�6.   $��	10,10,0,0q�6I   $	��Top=�6\   $��69��6t   �� 5      $��number of symbol:��6   $
��Left=�68   $��	88,10,0,0q�6S   $	��Top=�6f   �� 5      -   5        $ txtBxNumberOfSymbol��5      $
��Left=�6;   $��23��6V   $��	93,36,0,0q�6b   $
 Wrap=�6u   $	��Top=�6�   $	��105��6�   6�   �� 5      -   5         ��Name$ btnAdd��5      $	��Add��6-   $
��Left=�6;   $��
212,36,0,0q�6V   $	��Top=�6j   $��60��6�   $��23��6�   6�   �� 5      -   5         ��Name$ listNode��5      $
��Left=�61   $��88��6L   $��	10,72,0,0q�6X   $	��Top=�6k   $	��189��6�    �� View 5      �� 5      .P�5   "   �� $��Symbol��61   	 �� Width$	 60��6e   
 �� DisplayMemberBinding
 6A   �� *Id+6e   �� 5   "   $��Number of symbol��61   $		 123��6s   
 6K   �� *Number+6s   5      5      �� 5       -   5        $ 	btnRemove��5       $��Remove��60   $
��Left=�6A   $��212,111,0,0q�6\   $	��Top=�6q   $��60��6�   $��23��6�   6�   �� 5!      -	   5        $ btnEdit��5!      $
��Edit��6.   $
��Left=�6=   $��
212,82,0,0q�6X   $	��Top=�6l   $��60��6�   $��23��6�   �� 5"      -
   5         ��Name$ cmbBxInputAlphabet��5"      $
��Left=�6;   $��	10,36,0,0q�6V   $	��Top=�6i   $��73��6�   
���������5#      �� 5$       �� EndPoint$	 0,1&�62    �� 
StartPoint$	 0,0&�6A   .��5%   "   �  � Color$ 	#FFF0F0F0��6/    � Offset$ 0Q�6A   � 5&   "   $ White��6/   $ 1Q�6=   5'      5)      5*      5+      �� 5,      -   5         ��Name$ checkBxIfRandom��5,      $��Random graph��60   $
��Left=�6G   $��	10,15,0,0q�6b   $	��Top=�6u    d� 	IsChecked$
 TrueY�6�   6�   5-      �� 5.   
   -   5        $ btnClear��5.      $��Clear��6#   $��27,289,236,0q�63   $	��Top=�6I   $
��Bold"�6a   �� 5/   
   -   5        $ btnOk��5/      $��Ok��6    $
��Left=�6-   $��228,289,0,0q�6H   $	��Top=�6]   $��60��6u   $
��Bold"�6�   6�   51      52      