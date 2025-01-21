import axios from "axios";
import { UserProfileToken } from "../models/user";
import { handleError } from "../helpers/errorHandler";

const api = "http://localhost:5116/api/";

export const loginAPI = async (email: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/login", { email: email, password: password });
    return data;
  } catch (error) {
    handleError(error);
  }
};

export const registerAPI = async (email: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "account/register", { email: email, password: password });
    return data;
  } catch (error) {
    handleError(error);
  }
};
