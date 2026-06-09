def get_bulb_code(bulb_name):
    
    return (bulb_name[0:3]).upper()

def main():
    prices = {'daffodil': 0.35, 'tulip': 0.33, 'crocus': 0.25, 'hyacinth': 0.75, 'bluebell': 0.50}
    Mary = {'daffodil': 50, 'tulip': 100}
    prices['tulip'] = round((prices.get('tulip') * 1.25), 2)
    Mary['hyacinth'] = 30
    quantity_purchased = 0
    total = 0
    print('You have purchased the following bulbs:')
    for bulb in sorted(Mary.keys()):
        bulb_code = get_bulb_code(bulb)
        subtotal = round((Mary.get(bulb) * prices.get(bulb)), 2)
        total = total + subtotal
        quantity_purchased = quantity_purchased + Mary.get(bulb)
        print('{:<6} *{:>5} = {:6.2f}'.format(bulb_code, Mary.get(bulb), subtotal) )
    print('\n')
    print('Thank you for purchasing ' + str(quantity_purchased) + ' bulbs from Bluebell Greenhouses.')
    print('Your total comes to $ ' + str(total)+ '.')
main()