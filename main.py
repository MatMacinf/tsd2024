import random
from typing import List, TypeVar, Generic

T = TypeVar('T')

class MyList(Generic[T]):
    def __init__(self):
        self.items: List[T] = []

    def add(self, element: T):
        if random.randint(0,1) == 0:
            self.items.insert(0,element)
        else:
            self.items.append(element)

    def get(self, index: int) -> T:
        if not self.items:
            raise ValueError("List is empty")

        if index <0 or index >= len(self.items):
            raise IndexError("index out of range")

        random_index = random.randint(0,index)
        return self.items[random_index]

    def isEmpty(self) -> bool:
        return len(self.items) == 0


if __name__ == '__main__':
    rand_list = MyList[int]()
    print("List is empty:", rand_list.isEmpty())
    rand_list.add(10)
    rand_list.add(20)
    rand_list.add(30)
    rand_list.add(40)

    print("List is empty:", rand_list.isEmpty())
    print("Random element up to index 2:", rand_list.get(2))
    print("Random element up to index 3:", rand_list.get(3))


                # WITH BestBuy AS (
                #     SELECT * FROM GoldPrices
                #     WHERE YEAR(Date) BETWEEN 2020 AND 2024
                #     ORDER BY Price ASC
                #     LIMIT 1 )
                #
                # SELECT * FROM GoldPrices
                # WHERE Date > (SELECT Date FROM BestBuy)
                # ORDER BY Price DESC
                # LIMIT 1;
                #
                # is equivalent to
                # var bestBuy = (from p in _goldPrices
                #            where p.Date.Year >= 2020 & & p.Date.Year <= 2024
                #            orderby p.Price
                #            select p).FirstOrDefault();
                #
                # var bestSell = (from p in _goldPrices
                #             where p.Date > bestBuy.Date
                #             orderby p.Price descending
                #             select p).FirstOrDefault();