# csv obtained from https://plaid.com/documents/transactions-personal-finance-category-taxonomy.csv

import csv

file_template = """
namespace Spend.Graph.Domain.Entities.Transactions;

public static class DefaultTransactionCategories
{
    public static IReadOnlyList<DefaultTransactionCategory> AsList => _asList.Value;
    public static IReadOnlyDictionary<string, DefaultTransactionCategory> ByName => _byName.Value;

    private static readonly Lazy<IReadOnlyList<DefaultTransactionCategory>> _asList
        = new(() => new List<DefaultTransactionCategory>()
        {
<1>
        });
        

    private static readonly Lazy<IReadOnlyDictionary<string, DefaultTransactionCategory>> _byName
        = new(() => AsList
            .Concat(AsList.SelectMany(x =>
                x.ChildDefaultTransactionCategories ?? Array.Empty<DefaultTransactionCategory>()))
            .ToDictionary(x => x.Name));
}
"""

parent_template = """
                new (<1>, new DefaultTransactionCategory[]{<2>}),
"""

child_template = """                    new(<1>),"""


def main():
    with open('transactions-personal-finance-category-taxonomy.csv', 'r') as f:
        reader = csv.reader(f)
        ts = []
        for row in reader:
            ts.append(row)
        ts = ts[1:]
        parents = set([x[0] for x in ts])
        d = dict({x: [] for x in parents})
        for t in ts:
            d[t[0]].append(t)

        all_s = ""
        for parent in parents:
            parent_s = parent_template.replace("<1>", "\"{}\", null, null".format(parent))
            children = d[parent]
            children_s = "\n".join([child_template.replace("<1>", "\"{}\", \"{}\", \"{}\", null".format(child[1], child[2].replace('\"', ''), child[0])) for child in children])
            all_s += parent_s.replace("<2>", children_s)

        file_s = file_template.replace("<1>", all_s)

    with open('../../Graph/src/Graph/Domain/Entities/Transactions/DefaultTransactionCategories.cs', 'w') as f:
        f.write(file_s)


if __name__ == '__main__':
    main()
