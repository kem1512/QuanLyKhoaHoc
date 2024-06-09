"use client";

import BlogHandler from "../../../../components/Handler/BlogHandler";

export default function DashboardBlogUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <BlogHandler id={id} />;
}
