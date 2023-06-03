export interface HubResponse<T> extends HubBaseResponse {
  data?: T;
}

export interface HubBaseResponse {
  errorMessage?: string;
  success: boolean;
  validationErrors: ErrorResponse[];
}

export interface ErrorResponse {
  name: string;
  message: string;
}
