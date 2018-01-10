export interface Transaction {
    id: number;
    date: Date;
    merchant: string;
    description: string;
    alteration: number;
    balance: number;
}