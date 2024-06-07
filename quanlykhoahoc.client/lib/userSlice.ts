import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  user: null,
  accessToken: null,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    login: (state, action) => {
      state.user = action.payload;
    },
    logout: (state) => {
      state.user = null;
      state.accessToken = null;
    },
    setAccessToken: (state, action) => {
      state.accessToken = action.payload;
    },
  },
});

export const { login, logout, setAccessToken } = userSlice.actions;

export default userSlice.reducer;
