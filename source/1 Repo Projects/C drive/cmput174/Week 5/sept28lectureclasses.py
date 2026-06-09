class Account:
    def __init__(self, acc_id, acc_balance):
        # initalizes the instance attributes of the Account object
        # self is the account object, acc_id is of type string and represents the account number, acc_balance is of type float and represents the account balance.
        
        self.account_id = acc_id
        self.balance = acc_balance
    def display(self):
        print('Account Number: ' + self.account_id + ' Balance: ' + str(self.balance))
    def deposit(self, amount):
        self.balance = self.balance + amount
    def withdraw(self, amount):
        if self.balance > amount:
            self.balance = self.balance - amount
    def transfer(self, account_to, amount):
        self.withdraw(amount)
        account_to.deposit(amount)
        
        
def main():
    accountA = Account('01234', 500.01)
    accountB = Account('05678', 200.01)
    accountA.deposit(500.00)
    accountA.withdraw(750.00)
    accountA.display()
    accountB.display()    
    accountA.transfer(accountB, 100.00)
    accountA.display()
    accountB.display()
main()
        

## a calss definition characterizes an object. i.e a die. A function thtat exists inside a class is called a method. The init method is used to initialize the instance attributes of an object
#import random
#class Die:
    #def __init__(self, number_of_sides, colour):
        #self.sides = number_of_sides # an instance attribute is a property of a group of objects
        #self.colour = colour
    #def roll(self):
        #print(self.colour)
        #return random.randint(1, self.sides)
## program starts
#def main():
    #d6 = Die(6, 'red') # 1. object gets created 2. init method is called if it exists 3. Init method initializes the instance attributes in the method i.e a 6 sided object is created and bound to the d6 object. # self is essentially a pronoun to refer to the object that is created and using the init method. self is bound to d6 and d6 is the object it should roll.
    #d8 = Die(8, 'green') # self is bound to d6 when called on that line, and bound to d8 when called on that line. Thus you can create multiple objects, and number_of_sides is bound to whatever goes in the argument
    #d12 = Die(12, 'blue')
    #print(d6.roll())
    #print(d8.roll())
    #print(d12.roll())
    
#main()
        
        