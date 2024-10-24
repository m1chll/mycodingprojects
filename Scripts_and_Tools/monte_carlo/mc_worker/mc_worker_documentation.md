docker build -t mc_worker .

docker run --rm -d --network mc_network --name worker1 mc_worker

