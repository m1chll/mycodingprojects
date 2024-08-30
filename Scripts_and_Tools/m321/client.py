from flask import Flask, request, jsonify
 
app = Flask(__name__)
 
@app.route('/summe', methods=['GET'])
def summe():
    # Parameter "a" und "b" von der URL abfragen
    a = request.args.get('a', type=int)
    b = request.args.get('b', type=int)
   
    # Berechnung der Summe
    result = a + b
   
    # Rückgabe als JSON
    return jsonify({'summe': result})
 
if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
 