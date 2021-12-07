export class ChefDetails{
  chef: Chef;
  meals: Meal[];
}

export class Chef{
  id: string;
  firstName: string;
  lastName: string;
  address: string;
  latitude: number;
  cookingStyle: string;
  longitude: number;
}

export class Meal{
  id: string;
  name: string;
  description: string;
  price: number;
}
