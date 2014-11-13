Dim o
Set o = CreateObject("MSXML2.XMLHTTP")
o.open "GET", "http://ifconfig.me/ip", False
o.send
WScript.StdOut.Write o.responseText