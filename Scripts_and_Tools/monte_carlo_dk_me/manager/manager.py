from flask import Flask, request, jsonify

app = Flask(__name__)
results = []

@app.route('/submit', methods=['POST'])
def submit_result():
    data = request.json
    result = data.get('pi_estimate')
    if result:
        results.append(result)
        return jsonify({"message": "Result received"}), 200
    else:
        return jsonify({"message": "Invalid data"}), 400

@app.route('/average', methods=['GET'])
def get_average():
    if len(results) == 0:
        return jsonify({"average": None, "message": "No results yet"}), 200
    average = sum(results) / len(results)
    return jsonify({"average": average}), 200

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)