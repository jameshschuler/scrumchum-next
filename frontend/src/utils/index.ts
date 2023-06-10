import { UserRole } from "@/types/user";

export const userRoleOptions = () => {
  const keys = Object.keys(UserRole).filter((v) => isNaN(Number(v)));
  const optionsMap = keys.map((key: string) => {
    return {
      name: key.replace(/([a-z])([A-Z])/g, "$1 $2"),
      value: Number(UserRole[key as any]),
    };
  });
  return optionsMap;
};
