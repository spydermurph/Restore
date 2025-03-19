import { createSlice } from "@reduxjs/toolkit";

const getInitialDarkMode = () => {
  const storedDarkMode = localStorage.getItem("darkMode");
  return storedDarkMode ? JSON.parse(storedDarkMode) : false;
};

export const uiSlice = createSlice({
  name: "ui",
  initialState: {
    isLoading: false,
    darkMode: getInitialDarkMode(),
  },
  reducers: {
    startLoading: (state) => {
      state.isLoading = true;
    },
    stopLoading: (state) => {
      state.isLoading = false;
    },
    setDarkMode: (state) => {
      const newDarkMode = !state.darkMode;
      localStorage.setItem("darkMode", JSON.stringify(newDarkMode));
      state.darkMode = newDarkMode;
    },
  },
});

export const { startLoading, stopLoading, setDarkMode } = uiSlice.actions;
