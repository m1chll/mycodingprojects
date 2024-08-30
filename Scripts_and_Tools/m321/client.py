from flask import Flask, jsonify

app = Flask(__name__)

@app.route('/summe', methods=['GET'])
def summe():
    # Benutzer nach den Zahlen fragen
    zahl1 = float(input("Bitte gib die erste Zahl ein: "))
    zahl2 = float(input("Bitte gib die zweite Zahl ein: "))

    # Berechnung der Summe
    summe = zahl1 + zahl2

    # Antwort vorbereiten
    response_data = {'summe': summe, 'message': 'MichaelEpper'}

    # Antwort zurückgeben
    return jsonify(response_data)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
