docker run \
    --name testneo4j \
    -p7474:7474 -p7687:7687 \
    -d \
    -v C:/Desktop/neo4j/data:/data \
    -v C:/Desktop/neo4j/logs:/logs \
    --env NEO4J_AUTH=neo4j/test \
    neo4j:latest