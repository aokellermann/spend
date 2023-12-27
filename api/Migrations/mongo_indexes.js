use Graph

db.ItemLinks.createIndex({
    userId: 1
}, {
    unique: false
})

db.TransactionsSyncStates.createIndex({
    userId: 1
}, {
    unique: false
})

db.Transactions.createIndex({
    userId: 1
}, {
    unique: false
})

db.Transactions.createIndex({
    plaidTransactionId: 1
}, {
    unique: true
})
