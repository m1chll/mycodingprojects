from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/summe', methods=['GET'])
def summe():
    zahl1 = float(request.args.get('zahl1', default=0))
    zahl2 = float(request.args.get('zahl2', default=0))

    summe = zahl1 + zahl2

    response_data = {'summe': summe, 'message': 'MichaelEpper'}

    return jsonify(response_data)

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)