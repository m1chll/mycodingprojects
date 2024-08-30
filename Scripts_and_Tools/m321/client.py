def get_sum_from_service(number1, number2):
    url = "http://<URL_Ihres_Dienstes>"  # Ersetzen Sie dies durch die tatsächliche URL Ihres Dienstes
    headers = {'Content-Type': 'application/json'}
    
    # Erstellen des JSON-Objekts mit den Zahlen
    data = {
        "number1": number1,
        "number2": number2
    }
    
    # Senden der POST-Anfrage an den Dienst
    response = requests.post(url, headers=headers, data=json.dumps(data))
    
    if response.status_code == 200:
        # Extrahieren der Summe aus der Antwort
        result = response.json()
        return result.get('sum', 'No sum returned')  # 'sum' ist das angenommene Schlüsselwort
    else:
        return f"Error: {response.status_code}"

    if __name__ == "__main__":
        # Eingabe der Zahlen
        number1 = float(input("Geben Sie die erste Zahl ein: "))
        number2 = float(input("Geben Sie die zweite Zahl ein: "))
        
        # Abrufen der Summe vom Dienst
        result = get_sum_from_service(number1, number2)
        
        # Ausgabe des Ergebnisses
        print(f"Die Summe der Zahlen {number1} und {number2} beträgt: {result}")
