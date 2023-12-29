use Graph

db.ItemLinks.createIndex({
    userId: 1
}, {
    name: "ix_userId",
    unique: false
})

db.TransactionsSyncStates.createIndex({
    userId: 1
}, {
    name: "ix_userId",
    unique: false
})

db.Transactions.createIndex({
    userId: 1
}, {
    name: "ix_userId",
    unique: false
})

db.TransactionCategories.createIndex({
    userId: 1
}, {
    name: "ix_userId",
    unique: false
})
