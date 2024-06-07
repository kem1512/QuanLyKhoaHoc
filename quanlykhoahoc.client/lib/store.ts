// store/index.js
import { configureStore } from '@reduxjs/toolkit';
import userReducer from './userSlice';

const store = configureStore({
  reducer: {
    auth: userReducer,
  },
});

export default store;
