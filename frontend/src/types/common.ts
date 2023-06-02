export interface HubResponse<T> {
  data?: T;
  errorMessage?: string;
  success: boolean;
}

export interface HubBaseResponse {
  errorMessage?: string;
  success: boolean;
}
