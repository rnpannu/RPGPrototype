import random

n = input('Enter your number: ')
n = float(n)
sigma = (1/n)

while n > 1:
    n = n - 1
    sigma = sigma + (1/(n))
        
print(sigma)