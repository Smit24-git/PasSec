echo "Execution Started:" 
date 


sudo docker compose build 
sudo docker compose up -d 
sudo docker compose -f docker-compose.migration.yaml build 
sudo docker compose -f docker-compose.migration.yaml up 
sudo docker compose -f docker-compose.migration.yaml down 

echo "Execution Finished: " 
date 

echo "-------------------------" 

# notepad output.txt

