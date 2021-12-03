import { Address } from "src/app/shared/models/address";

export interface CreateChefRequest {
  firstName: string;
  lastName: string;
  address: Address;
  cookingStyle: string;
}
