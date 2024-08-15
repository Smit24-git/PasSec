echo "Execution Started:" |& tee -a output.txt
date |& tee -a output.txt


docker compose build |& tee -a output.txt
docker compose up -d |& tee -a output.txt
docker compose -f docker-compose.migration.yaml build |& tee -a output.txt
docker compose -f docker-compose.migration.yaml up |& tee -a output.txt
docker compose -f docker-compose.migration.yaml down |& tee -a output.txt

echo "Execution Finished: " |& tee -a output.txt
date |& tee -a output.txt

echo "-------------------------" |& tee -a output.txt

# notepad output.txt

