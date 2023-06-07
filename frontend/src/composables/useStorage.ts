export default function useStorage() {
  function storeUserId(userId: number) {
    localStorage.setItem("userId", JSON.stringify(userId));
  }

  function getUser(): number | null {
    const userId = localStorage.getItem("userId");
    return userId ? parseInt(userId) : null;
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
