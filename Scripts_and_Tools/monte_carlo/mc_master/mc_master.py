import socket
import threading

# Manager-Server Klasse
class PiManager:
    def __init__(self, host='0.0.0.0', port=65432, total_samples=1000000):
        self.host = host
        self.port = port
        self.total_samples = total_samples
        self.worker_results = []
        self.lock = threading.Lock()

    def handle_worker(self, conn, addr):
        print(f"Worker connected from {addr}")

        # Verteile die Anzahl der Samples pro Worker
        samples_per_worker = self.total_samples // len(self.worker_results)

        try:
            conn.sendall(str(samples_per_worker).encode())
            result = conn.recv(1024).decode()
            pi_part = float(result)

            with self.lock:
                self.worker_results.append(pi_part)
        except Exception as e:
            print(f"Error with worker {addr}: {e}")
        finally:
            conn.close()

    def start_server(self):
        with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as server_socket:
            server_socket.bind((self.host, self.port))
            server_socket.listen()
            print(f"PiManager listening on {self.host}:{self.port}")

            while len(self.worker_results) < number_of_workers:
                conn, addr = server_socket.accept()
                worker_thread = threading.Thread(target=self.handle_worker, args=(conn, addr))
                worker_thread.start()

            # Berechne das finale Ergebnis
            total_inside_circle = sum(self.worker_results)
            pi_estimate = (4 * total_inside_circle) / self.total_samples
            print(f"Final Pi estimate: {pi_estimate}")

# Starte den Manager mit einer definierten Anzahl von Workern
if __name__ == "__main__":
    number_of_workers = 3  # Ändere diese Zahl je nach Anzahl der Raspberry Pis
    manager = PiManager(total_samples=1000000)
    manager.start_server()
   