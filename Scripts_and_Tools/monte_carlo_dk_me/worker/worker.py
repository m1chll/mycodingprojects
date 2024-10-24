import random
import requests
import json
import time


def monte_carlo_pi(n):
    inside_circle = 0
    for _ in range(n):
        x, y = random.random(), random.random()
        if x**2 + y**2 <= 1:
            inside_circle += 1
    return (4 * inside_circle) / n

def send_result_to_manager(pi_estimate):
    url = 'http://manager:5000/submit'
    data = {'pi_estimate': pi_estimate}
    headers = {'Content-Type': 'application/json'}
    response = requests.post(url, data=json.dumps(data), headers=headers)
    if response.status_code == 200:
        print("Ergebnis erfolgreich gesendet.")
    else:
        print("Fehler beim Senden des Ergebnisses.")

if __name__ == '__main__':
    n = 1000000  # Anzahl der Punkte für die Pi-Näherung
    while True:
        pi_estimate = monte_carlo_pi(n)
        send_result_to_manager(pi_estimate)
        time.sleep(5)  # Warte 5 Sekunden bevor die Berechnung wiederholt wird

