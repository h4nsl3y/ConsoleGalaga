import { defineConfig } from 'vite'
import react, { reactCompilerPreset } from '@vitejs/plugin-react'
import babel from '@rolldown/plugin-babel'
import tailwindcss from '@tailwindcss/vite';
import path from 'path';

// https://vite.dev/config/
export default defineConfig({
  base: './',
  plugins: [
    react(),
    tailwindcss(),
    babel({ presets: [reactCompilerPreset()] }),
    {
      name: 'no-module',
      transformIndexHtml(html) {
        return html
          .replace(' type="module" crossorigin', ' defer')
          .replace(' crossorigin', '');
      },
    },
  ],
  resolve: {
    alias:[{ find: "@", replacement: path.resolve(__dirname, "src")}]
  },
  build: {
    cssCodeSplit: false,
    rollupOptions: {
      output: {
        format: "iife",
        inlineDynamicImports: true,
        entryFileNames: "app.js",
        chunkFileNames: "app.js",
        assetFileNames: "assets/[name][extname]",
      },
    },
  },
})
