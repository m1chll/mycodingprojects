docker network create mc_network

docker build -t mc_manager .

docker run --rm -d --network mc_network --name manager mc_manager

