FROM python:3.9-slim
WORKDIR /app
COPY worker.py /app/
RUN pip install requests
CMD ["python", "worker.py"]