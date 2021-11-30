
;Buchstaben mit Hotkey belegen und Funktion aufrufen
;Linke Hand
Hotkey, s, sendAuftrag
Hotkey, d, placeSteine
Hotkey, f, placeBretter

;Rechte Hand
Hotkey, j, placeSand
Hotkey, k, placeZement
Hotkey, l, sendAuftrag

return


placeSteine:
MouseClick, left,  414,  517
return

placeBretter:
MouseClick, left,  580,  455
return

placeSand:
MouseClick, left,  779,  526
return

placeZement:
MouseClick, left,  1155,  510
return

sendAuftrag:
MouseClick, left,  1080,  311
return
