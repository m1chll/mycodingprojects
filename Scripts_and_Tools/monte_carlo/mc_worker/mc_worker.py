import socket
import random

def monte_carlo_pi(samples):
    inside_circle = 0
    for _ in range(samples):
        x = random.random()
        y = random.random()
        if x**2 + y**2 <= 1:
            inside_circle += 1
    return inside_circle

def connect_to_manager(host, port):
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as client_socket:
        client_socket.connect((host, port))

        # Empfange die Anzahl der Samples vom Manager
        samples = int(client_socket.recv(1024).decode())
        print(f"Received {samples} samples to process.")

        # Führe die Monte-Carlo-Simulation durch
        inside_circle = monte_carlo_pi(samples)

        # Sende das Ergebnis zurück an den Manager
        client_socket.sendall(str(inside_circle).encode())
        print(f"Sent result {inside_circle} to manager.")

# Verbindung mit dem Manager-Server herstellen
if __name__ == "__main__":
    manager_host = "manager"  # Docker-Container-Name des Managers
    manager_port = 65432
    connect_to_manager(manager_host, manager_port)
