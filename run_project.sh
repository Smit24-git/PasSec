echo "Execution Started:" 
date 


docker compose build 
docker compose up -d 
docker compose -f docker-compose.migration.yaml build 
docker compose -f docker-compose.migration.yaml up 
docker compose -f docker-compose.migration.yaml down 

echo "Execution Finished: " 
date 

echo "-------------------------" 

# notepad output.txt

