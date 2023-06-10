export default function useStorage() {
  function storeUserId(userId: string) {
    localStorage.setItem("userId", JSON.stringify(userId));
  }

  function getUser(): string | null {
    const userId = localStorage.getItem("userId");
    return userId ? JSON.parse(userId) : null;
  }

  function clearUser() {
    localStorage.removeItem("userId");
  }

  return {
    clearUser,
    getUser,
    storeUserId,
  };
}
