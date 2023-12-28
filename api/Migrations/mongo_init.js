db.createUser(
    {
        user: "graph",
        pwd: process.env["MONGO_GRAPH_PASSWORD"],
        roles: [
            {
                role: "readWrite",
                db: "Graph"
            }
        ]
    }
);