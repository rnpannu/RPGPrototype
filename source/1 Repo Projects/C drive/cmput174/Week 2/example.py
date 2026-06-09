
def display_message(message, symbol):
    symbols = symbol * len(message)
    print(symbols)
    print(message)
    print(symbols)


def main():
    user_account_number = 'AC-1234'
    user_password = 2468
    max_amount = 500
    error_message = 'Transaction Cancelled'
    error_symbol = "*"
    success_message = 'Transaction Processed'
    success_symbol = '!' 
    account_number = input('Enter account number: ')
    if account_number != user_account_number:
        display_message(error_message, error_symbol)
    else:
        password = int(input('Enter password: '))
        if password != user_password:
            display_message(error_message, error_symbol)
        else:
            amount = float(input('Enter the amount you wish to withdraw: '))
            if amount > max_amount:
                display_message(error_message, error_symbol)
            else:
                display_message(success_message, success_symbol)
main()