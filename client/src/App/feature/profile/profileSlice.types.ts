import { Option } from "../../../pages/Profile/components/Edit/Edit.types";

interface IProfile {
  username?: string;
  email?: string;
  firstName?: string;
  lastName?: string;
  avatar?: File | string;
  contactEmail?: string;
  phoneNumber?: string;
  saveAddress?: boolean;
  shippingAddress?: {
    address?: string;
    country?: string;
    state?: string;
    city?: string;
    postalCode?: number;
  };
}

type TGetProfileResponse = IProfile & {
  shippingAddress?: {
    address?: string;
    country?: Option;
    state?: Option;
    city?: Option;
    postalCode?: number;
  };
};

export type { IProfile, TGetProfileResponse };
