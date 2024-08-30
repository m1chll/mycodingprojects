from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/add', methods=['GET'])
def add():
    try:
        zahl1 = float(request.args.get('zahl1'))
        zahl2 = float(request.args.get('zahl2'))
        result = zahl1 + zahl2
        return jsonify({'result': result})
    except (TypeError, ValueError):
        return jsonify({'error': 'Invalid input'}), 400

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
