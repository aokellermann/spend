
namespace Spend.Graph.Domain.Entities.Transactions;

/// <summary>
///     Default transaction categories. These are provided by Plaid's personal finance API.
///     Note that this file is autogenerated (see parent CodeGeneration folder).
/// </summary>
public static class DefaultTransactionCategories
{
    public const string UnknownCategoryName = "UNKNOWN";
    public static IReadOnlyList<DefaultTransactionCategory> AsList => _asList.Value;
    public static IReadOnlyDictionary<string, DefaultTransactionCategory> ByName => _byName.Value;

    private static readonly Lazy<IReadOnlyList<DefaultTransactionCategory>> _asList
        = new(() => new List<DefaultTransactionCategory>()
        {
            new(UnknownCategoryName, null, null, Array.Empty<DefaultTransactionCategory>()),

            new("TRANSFER_IN", null, null, new DefaultTransactionCategory[]{
                new("TRANSFER_IN_CASH_ADVANCES_AND_LOANS", "Loans and cash advances deposited into a bank account", "TRANSFER_IN", null),
                new("TRANSFER_IN_DEPOSIT", "Cash, checks, and ATM deposits into a bank account", "TRANSFER_IN", null),
                new("TRANSFER_IN_INVESTMENT_AND_RETIREMENT_FUNDS", "Inbound transfers to an investment or retirement account", "TRANSFER_IN", null),
                new("TRANSFER_IN_SAVINGS", "Inbound transfers to a savings account", "TRANSFER_IN", null),
                new("TRANSFER_IN_ACCOUNT_TRANSFER", "General inbound transfers from another account", "TRANSFER_IN", null),
                new("TRANSFER_IN_OTHER_TRANSFER_IN", "Other miscellaneous inbound transactions", "TRANSFER_IN", null),
            }),

            new("RENT_AND_UTILITIES", null, null, new DefaultTransactionCategory[]{
                new("RENT_AND_UTILITIES_GAS_AND_ELECTRICITY", "Gas and electricity bills", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_INTERNET_AND_CABLE", "Internet and cable bills", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_RENT", "Rent payment", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_SEWAGE_AND_WASTE_MANAGEMENT", "Sewage and garbage disposal bills", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_TELEPHONE", "Cell phone bills", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_WATER", "Water bills", "RENT_AND_UTILITIES", null),
                new("RENT_AND_UTILITIES_OTHER_UTILITIES", "Other miscellaneous utility bills", "RENT_AND_UTILITIES", null),
            }),

            new("HOME_IMPROVEMENT", null, null, new DefaultTransactionCategory[]{
                new("HOME_IMPROVEMENT_FURNITURE", "Furniture, bedding, and home accessories", "HOME_IMPROVEMENT", null),
                new("HOME_IMPROVEMENT_HARDWARE", "Building materials, hardware stores, paint, and wallpaper", "HOME_IMPROVEMENT", null),
                new("HOME_IMPROVEMENT_REPAIR_AND_MAINTENANCE", "Plumbing, lighting, gardening, and roofing", "HOME_IMPROVEMENT", null),
                new("HOME_IMPROVEMENT_SECURITY", "Home security system purchases", "HOME_IMPROVEMENT", null),
                new("HOME_IMPROVEMENT_OTHER_HOME_IMPROVEMENT", "Other miscellaneous home purchases, including pool installation and pest control", "HOME_IMPROVEMENT", null),
            }),

            new("PERSONAL_CARE", null, null, new DefaultTransactionCategory[]{
                new("PERSONAL_CARE_GYMS_AND_FITNESS_CENTERS", "Gyms, fitness centers, and workout classes", "PERSONAL_CARE", null),
                new("PERSONAL_CARE_HAIR_AND_BEAUTY", "Manicures, haircuts, waxing, spa/massages, and bath and beauty products ", "PERSONAL_CARE", null),
                new("PERSONAL_CARE_LAUNDRY_AND_DRY_CLEANING", "Wash and fold, and dry cleaning expenses", "PERSONAL_CARE", null),
                new("PERSONAL_CARE_OTHER_PERSONAL_CARE", "Other miscellaneous personal care, including mental health apps and services", "PERSONAL_CARE", null),
            }),

            new("TRANSPORTATION", null, null, new DefaultTransactionCategory[]{
                new("TRANSPORTATION_BIKES_AND_SCOOTERS", "Bike and scooter rentals", "TRANSPORTATION", null),
                new("TRANSPORTATION_GAS", "Purchases at a gas station", "TRANSPORTATION", null),
                new("TRANSPORTATION_PARKING", "Parking fees and expenses", "TRANSPORTATION", null),
                new("TRANSPORTATION_PUBLIC_TRANSIT", "Public transportation, including rail and train, buses, and metro", "TRANSPORTATION", null),
                new("TRANSPORTATION_TAXIS_AND_RIDE_SHARES", "Taxi and ride share services", "TRANSPORTATION", null),
                new("TRANSPORTATION_TOLLS", "Toll expenses", "TRANSPORTATION", null),
                new("TRANSPORTATION_OTHER_TRANSPORTATION", "Other miscellaneous transportation expenses", "TRANSPORTATION", null),
            }),

            new("TRAVEL", null, null, new DefaultTransactionCategory[]{
                new("TRAVEL_FLIGHTS", "Airline expenses", "TRAVEL", null),
                new("TRAVEL_LODGING", "Hotels, motels, and hosted accommodation such as Airbnb", "TRAVEL", null),
                new("TRAVEL_RENTAL_CARS", "Rental cars, charter buses, and trucks", "TRAVEL", null),
                new("TRAVEL_OTHER_TRAVEL", "Other miscellaneous travel expenses", "TRAVEL", null),
            }),

            new("GOVERNMENT_AND_NON_PROFIT", null, null, new DefaultTransactionCategory[]{
                new("GOVERNMENT_AND_NON_PROFIT_DONATIONS", "Charitable, political, and religious donations", "GOVERNMENT_AND_NON_PROFIT", null),
                new("GOVERNMENT_AND_NON_PROFIT_GOVERNMENT_DEPARTMENTS_AND_AGENCIES", "Government departments and agencies, such as driving licences, and passport renewal", "GOVERNMENT_AND_NON_PROFIT", null),
                new("GOVERNMENT_AND_NON_PROFIT_TAX_PAYMENT", "Tax payments, including income and property taxes", "GOVERNMENT_AND_NON_PROFIT", null),
                new("GOVERNMENT_AND_NON_PROFIT_OTHER_GOVERNMENT_AND_NON_PROFIT", "Other miscellaneous government and non-profit agencies", "GOVERNMENT_AND_NON_PROFIT", null),
            }),

            new("GENERAL_SERVICES", null, null, new DefaultTransactionCategory[]{
                new("GENERAL_SERVICES_ACCOUNTING_AND_FINANCIAL_PLANNING", "Financial planning, and tax and accounting services", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_AUTOMOTIVE", "Oil changes, car washes, repairs, and towing", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_CHILDCARE", "Babysitters and daycare", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_CONSULTING_AND_LEGAL", "Consulting and legal services", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_EDUCATION", "Elementary, high school, professional schools, and college tuition", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_INSURANCE", "Insurance for auto, home, and healthcare", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_POSTAGE_AND_SHIPPING", "Mail, packaging, and shipping services", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_STORAGE", "Storage services and facilities", "GENERAL_SERVICES", null),
                new("GENERAL_SERVICES_OTHER_GENERAL_SERVICES", "Other miscellaneous services, including advertising and cloud storage ", "GENERAL_SERVICES", null),
            }),

            new("GENERAL_MERCHANDISE", null, null, new DefaultTransactionCategory[]{
                new("GENERAL_MERCHANDISE_BOOKSTORES_AND_NEWSSTANDS", "Books, magazines, and news ", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_CLOTHING_AND_ACCESSORIES", "Apparel, shoes, and jewelry", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_CONVENIENCE_STORES", "Purchases at convenience stores", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_DEPARTMENT_STORES", "Retail stores with wide ranges of consumer goods, typically specializing in clothing and home goods", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_DISCOUNT_STORES", "Stores selling goods at a discounted price", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_ELECTRONICS", "Electronics stores and websites", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_GIFTS_AND_NOVELTIES", "Photo, gifts, cards, and floral stores", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_OFFICE_SUPPLIES", "Stores that specialize in office goods", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_ONLINE_MARKETPLACES", "Multi-purpose e-commerce platforms such as Etsy, Ebay and Amazon", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_PET_SUPPLIES", "Pet supplies and pet food", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_SPORTING_GOODS", "Sporting goods, camping gear, and outdoor equipment", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_SUPERSTORES", "Superstores such as Target and Walmart, selling both groceries and general merchandise", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_TOBACCO_AND_VAPE", "Purchases for tobacco and vaping products", "GENERAL_MERCHANDISE", null),
                new("GENERAL_MERCHANDISE_OTHER_GENERAL_MERCHANDISE", "Other miscellaneous merchandise, including toys, hobbies, and arts and crafts", "GENERAL_MERCHANDISE", null),
            }),

            new("TRANSFER_OUT", null, null, new DefaultTransactionCategory[]{
                new("TRANSFER_OUT_INVESTMENT_AND_RETIREMENT_FUNDS", "Transfers to an investment or retirement account, including investment apps such as Acorns, Betterment", "TRANSFER_OUT", null),
                new("TRANSFER_OUT_SAVINGS", "Outbound transfers to savings accounts", "TRANSFER_OUT", null),
                new("TRANSFER_OUT_WITHDRAWAL", "Withdrawals from a bank account", "TRANSFER_OUT", null),
                new("TRANSFER_OUT_ACCOUNT_TRANSFER", "General outbound transfers to another account", "TRANSFER_OUT", null),
                new("TRANSFER_OUT_OTHER_TRANSFER_OUT", "Other miscellaneous outbound transactions", "TRANSFER_OUT", null),
            }),

            new("MEDICAL", null, null, new DefaultTransactionCategory[]{
                new("MEDICAL_DENTAL_CARE", "Dentists and general dental care", "MEDICAL", null),
                new("MEDICAL_EYE_CARE", "Optometrists, contacts, and glasses stores", "MEDICAL", null),
                new("MEDICAL_NURSING_CARE", "Nursing care and facilities", "MEDICAL", null),
                new("MEDICAL_PHARMACIES_AND_SUPPLEMENTS", "Pharmacies and nutrition shops", "MEDICAL", null),
                new("MEDICAL_PRIMARY_CARE", "Doctors and physicians", "MEDICAL", null),
                new("MEDICAL_VETERINARY_SERVICES", "Prevention and care procedures for animals", "MEDICAL", null),
                new("MEDICAL_OTHER_MEDICAL", "Other miscellaneous medical, including blood work, hospitals, and ambulances", "MEDICAL", null),
            }),

            new("ENTERTAINMENT", null, null, new DefaultTransactionCategory[]{
                new("ENTERTAINMENT_CASINOS_AND_GAMBLING", "Gambling, casinos, and sports betting", "ENTERTAINMENT", null),
                new("ENTERTAINMENT_MUSIC_AND_AUDIO", "Digital and in-person music purchases, including music streaming services", "ENTERTAINMENT", null),
                new("ENTERTAINMENT_SPORTING_EVENTS_AMUSEMENT_PARKS_AND_MUSEUMS", "Purchases made at sporting events, music venues, concerts, museums, and amusement parks", "ENTERTAINMENT", null),
                new("ENTERTAINMENT_TV_AND_MOVIES", "In home movie streaming services and movie theaters", "ENTERTAINMENT", null),
                new("ENTERTAINMENT_VIDEO_GAMES", "Digital and in-person video game purchases", "ENTERTAINMENT", null),
                new("ENTERTAINMENT_OTHER_ENTERTAINMENT", "Other miscellaneous entertainment purchases, including night life and adult entertainment", "ENTERTAINMENT", null),
            }),

            new("INCOME", null, null, new DefaultTransactionCategory[]{
                new("INCOME_DIVIDENDS", "Dividends from investment accounts", "INCOME", null),
                new("INCOME_INTEREST_EARNED", "Income from interest on savings accounts", "INCOME", null),
                new("INCOME_RETIREMENT_PENSION", "Income from pension payments ", "INCOME", null),
                new("INCOME_TAX_REFUND", "Income from tax refunds", "INCOME", null),
                new("INCOME_UNEMPLOYMENT", "Income from unemployment benefits, including unemployment insurance and healthcare", "INCOME", null),
                new("INCOME_WAGES", "Income from salaries, gig-economy work, and tips earned", "INCOME", null),
                new("INCOME_OTHER_INCOME", "Other miscellaneous income, including alimony, social security, child support, and rental", "INCOME", null),
            }),

            new("LOAN_PAYMENTS", null, null, new DefaultTransactionCategory[]{
                new("LOAN_PAYMENTS_CAR_PAYMENT", "Car loans and leases", "LOAN_PAYMENTS", null),
                new("LOAN_PAYMENTS_CREDIT_CARD_PAYMENT", "Payments to a credit card. These are positive amounts for credit card subtypes and negative for depository subtypes", "LOAN_PAYMENTS", null),
                new("LOAN_PAYMENTS_PERSONAL_LOAN_PAYMENT", "Personal loans, including cash advances and buy now pay later repayments", "LOAN_PAYMENTS", null),
                new("LOAN_PAYMENTS_MORTGAGE_PAYMENT", "Payments on mortgages", "LOAN_PAYMENTS", null),
                new("LOAN_PAYMENTS_STUDENT_LOAN_PAYMENT", "Payments on student loans. For college tuition, refer to General Services - Education", "LOAN_PAYMENTS", null),
                new("LOAN_PAYMENTS_OTHER_PAYMENT", "Other miscellaneous debt payments", "LOAN_PAYMENTS", null),
            }),

            new("FOOD_AND_DRINK", null, null, new DefaultTransactionCategory[]{
                new("FOOD_AND_DRINK_BEER_WINE_AND_LIQUOR", "Beer, Wine & Liquor Stores", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_COFFEE", "Purchases at coffee shops or cafes", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_FAST_FOOD", "Dining expenses for fast food chains", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_GROCERIES", "Purchases for fresh produce and groceries, including farmers' markets", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_RESTAURANT", "Dining expenses for restaurants, bars, gastropubs, and diners", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_VENDING_MACHINES", "Purchases made at vending machine operators", "FOOD_AND_DRINK", null),
                new("FOOD_AND_DRINK_OTHER_FOOD_AND_DRINK", "Other miscellaneous food and drink, including desserts, juice bars, and delis", "FOOD_AND_DRINK", null),
            }),

            new("BANK_FEES", null, null, new DefaultTransactionCategory[]{
                new("BANK_FEES_ATM_FEES", "Fees incurred for out-of-network ATMs", "BANK_FEES", null),
                new("BANK_FEES_FOREIGN_TRANSACTION_FEES", "Fees incurred on non-domestic transactions", "BANK_FEES", null),
                new("BANK_FEES_INSUFFICIENT_FUNDS", "Fees relating to insufficient funds", "BANK_FEES", null),
                new("BANK_FEES_INTEREST_CHARGE", "Fees incurred for interest on purchases, including not-paid-in-full or interest on cash advances", "BANK_FEES", null),
                new("BANK_FEES_OVERDRAFT_FEES", "Fees incurred when an account is in overdraft", "BANK_FEES", null),
                new("BANK_FEES_OTHER_BANK_FEES", "Other miscellaneous bank fees", "BANK_FEES", null),
            }),

        });
        

    private static readonly Lazy<IReadOnlyDictionary<string, DefaultTransactionCategory>> _byName
        = new(() => AsList
            .Concat(AsList.SelectMany(x =>
                x.ChildDefaultTransactionCategories ?? Array.Empty<DefaultTransactionCategory>()))
            .ToDictionary(x => x.Name));
}