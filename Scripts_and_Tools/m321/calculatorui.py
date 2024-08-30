import tkinter as tk
import requests

# IP-Adressen für die verschiedenen Operationen
ip_addresses = {
    'add': '10.27.160.13',
    'sub': '192.168.1.11',
    'mul': '192.168.1.12',
    'div': '192.168.1.13'
}

def get_result(operation):
    zahl1 = float(entry_1.get())
    zahl2 = float(entry_2.get())
    url = f'http://{ip_addresses[operation]}:5000/{operation}'
    params = {'zahl1': zahl1, 'zahl2': zahl2}
    response = requests.get(url, params=params)
    
    if response.status_code == 200:
        result = response.json().get('result', 'Fehler')
        return result
    else:
        return "Fehler bei der Berechnung"

def addieren():
    result = get_result('add')
    label_result.config(text=f"Ergebnis: {result}")

def subtrahieren():
    result = get_result('sub')
    label_result.config(text=f"Ergebnis: {result}")

def multiplizieren():
    result = get_result('mul')
    label_result.config(text=f"Ergebnis: {result}")

def dividieren():
    result = get_result('div')
    label_result.config(text=f"Ergebnis: {result}")

# GUI Setup
root = tk.Tk()
root.title("Einfacher Taschenrechner")

# Eingabefelder
tk.Label(root, text="Erste Zahl:").grid(row=0, column=0)
entry_1 = tk.Entry(root)
entry_1.grid(row=0, column=1)

tk.Label(root, text="Zweite Zahl:").grid(row=1, column=0)
entry_2 = tk.Entry(root)
entry_2.grid(row=1, column=1)

# Ergebnis Label
label_result = tk.Label(root, text="Ergebnis: ")
label_result.grid(row=2, column=0, columnspan=2)

# Buttons für Operationen
tk.Button(root, text="+", command=addieren).grid(row=3, column=0)
tk.Button(root, text="-", command=subtrahieren).grid(row=3, column=1)
tk.Button(root, text="*", command=multiplizieren).grid(row=4, column=0)
tk.Button(root, text="/", command=dividieren).grid(row=4, column=1)

root.mainloop()
