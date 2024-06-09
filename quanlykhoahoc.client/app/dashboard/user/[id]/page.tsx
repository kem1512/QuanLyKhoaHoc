"use client";

import UserHandler from "../../../../components/Handler/UserHandler";

export default function DashboardUserUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <UserHandler id={id} />;
}
