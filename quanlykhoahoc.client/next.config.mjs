/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: false,
  swcMinify: true,
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: `${process.env.WEBSITE_URL}/api/:path*`,
      },
      {
        source: "/images/:path*",
        destination: `${process.env.WEBSITE_URL}/images/:path*`,
      },
    ];
  },
  publicRuntimeConfig: {
    NEXT_PUBLIC_WEBSITE_TITLE: process.env.NEXT_PUBLIC_WEBSITE_TITLE,
  },
};

export default nextConfig;
