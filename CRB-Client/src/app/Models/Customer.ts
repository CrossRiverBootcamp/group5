export interface Customer {
    firstName: string;
    lastName: string ;
    email: string;
    password: string ;
    verificationCode?: number|string ;
}